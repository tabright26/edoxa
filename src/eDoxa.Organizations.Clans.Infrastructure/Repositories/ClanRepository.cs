// Filename: ClanRepository.cs
// Date Created: 2019-09-16
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Organizations.Clans.Domain.Repositories;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Organizations.Clans.Infrastructure.Repositories
{
    public class ClanRepository : IClanRepository
    {
        private readonly ClansDbContext _dbContext;

        public ClanRepository(ClansDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public void Create(Clan clan)
        {
            _dbContext.Clans.Add(clan);
        }

        public void Delete(Clan clan)
        {
            _dbContext.Clans.Remove(clan);
        }

        public async Task<IReadOnlyCollection<Clan>> FetchClansAsync()
        {
            return await _dbContext.Clans.ToListAsync();
        }

        public async Task<Clan?> FindClanAsync(ClanId clanId)
        {
            return await _dbContext.Clans.Include(clan => clan.Members).SingleOrDefaultAsync(clan => clan.Id == clanId);
        }

        public async Task<FileStream?> GetLogoAsync(ClanId clanId)
        {
            throw new NotImplementedException();
        }

        public async Task CreateOrUpdateLogoAsync(FileStream logo)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyCollection<Member>> FetchMembersAsync(ClanId clanId)
        {
            return await _dbContext.Members.AsNoTracking().Where(member => member.ClanId == clanId).ToListAsync();
        }
        public async Task<Member?> FindMemberAsync(ClanId clanId, MemberId memberId)
        {
            return await _dbContext.Members.SingleOrDefaultAsync(member => member.Id == memberId && member.ClanId == clanId );
        }

        public async Task CommitAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
