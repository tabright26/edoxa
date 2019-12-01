// Filename: ClanRepository.cs
// Date Created: 2019-10-06
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Clans.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Storage.Azure.Extensions;

using LinqKit;

using Microsoft.Azure.Storage;
using Microsoft.Azure.Storage.Blob;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Clans.Infrastructure.Repositories
{
    public class ClanRepository : IClanRepository
    {
        private readonly ClansDbContext _context;
        private readonly CloudStorageAccount _storageAccount;

        public ClanRepository(ClansDbContext context, CloudStorageAccount storageAccount)
        {
            _context = context;
            _storageAccount = storageAccount;
        }

        public IUnitOfWork UnitOfWork => _context;

        public void Create(Clan clan)
        {
            _context.Clans.Add(clan);
        }

        public void Delete(Clan clan)
        {
            _context.Clans.Remove(clan);
        }

        public async Task<IReadOnlyCollection<Clan>> FetchClansAsync()
        {
            return await _context.Clans.Include(clan => clan.Members).AsExpandable().ToListAsync();
        }

        public async Task<Clan?> FindClanAsync(ClanId clanId)
        {
            return await _context.Clans.Include(clan => clan.Members).AsExpandable().SingleOrDefaultAsync(clan => clan.Id == clanId);
        }

        public async Task<bool> ExistsAsync(string name)
        {
            return await _context.Clans.AsExpandable().AnyAsync(clan => clan.Name == name);
        }

        public async Task<Stream> DownloadLogoAsync(ClanId clanId)
        {
            var container = _storageAccount.GetBlobContainer();

            var directory = container.GetDirectoryReference($"organizations/clans/{clanId}/logo");

            var blobList = directory.ListBlobs().ToList();

            var memoryStream = new MemoryStream();

            if (blobList.Any())
            {
                var blobItem = blobList.OrderByDescending(item => long.Parse(Path.GetFileNameWithoutExtension(item.Uri.ToString()))).First();
                
                var blockBlob = directory.GetBlockBlobReference(Path.GetFileName(blobItem.Uri.ToString()));

                await blockBlob.DownloadToStreamAsync(memoryStream);
            }

            return memoryStream;
        }

        public async Task UploadLogoAsync(ClanId clanId, Stream stream, string fileName)
        {
            var container = _storageAccount.GetBlobContainer();

            var directory = container.GetDirectoryReference($"organizations/clans/{clanId}/logo");

            var blockBlob = directory.GetBlockBlobReference($"{new DateTimeOffset(DateTime.UtcNow).ToUnixTimeSeconds()}{Path.GetExtension(fileName)}");

            await blockBlob.UploadFromStreamAsync(stream);
        }

        public async Task DeleteLogoAsync(ClanId clanId)
        {
            var container = _storageAccount.GetBlobContainer();

            var directory = container.GetDirectoryReference($"organizations/clans/{clanId}/logo");

            foreach (var blockBlob in directory.ListBlobs().Cast<CloudBlockBlob>().ToList())
            {
                await blockBlob.DeleteIfExistsAsync();
            }
        }

        public async Task<IReadOnlyCollection<Member>> FetchMembersAsync(ClanId clanId)
        {
            return await _context.Members.AsExpandable().AsNoTracking().Where(member => member.ClanId == clanId).ToListAsync();
        }

        public async Task<Member?> FindMemberAsync(ClanId clanId, MemberId memberId)
        {
            return await _context.Members.AsExpandable().SingleOrDefaultAsync(member => member.ClanId == clanId && member.Id == memberId);
        }

        public async Task<bool> IsMemberAsync(UserId userId)
        {
            return await _context.Members.AsExpandable().AnyAsync(member => member.UserId == userId);
        }

        public async Task<bool> IsOwnerAsync(ClanId clanId, UserId ownerId)
        {
            return await _context.Clans.AsExpandable().AnyAsync(clan => clan.Id == clanId && clan.OwnerId == ownerId);
        }

        public async Task<IReadOnlyCollection<Division>> FetchDivisionsAsync(ClanId clanId)
        {
            return await _context.Divisions.AsExpandable().AsNoTracking().Where(division => division.ClanId == clanId).ToListAsync();
        }

        public async Task<Division?> FindDivisionAsync(DivisionId divisionId)
        {
            return await _context.Divisions.Include(clan => clan.Members).AsExpandable().SingleOrDefaultAsync(division => division.Id == divisionId);
        }

    }
}
