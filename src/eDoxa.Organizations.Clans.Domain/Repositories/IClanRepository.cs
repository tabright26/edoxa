﻿// Filename: IClanRepository.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Organizations.Clans.Domain.Repositories
{
    public interface IClanRepository
    {
        IUnitOfWork UnitOfWork { get; }

        void Create(Clan clan);

        void Delete(Clan clan);

        Task<IReadOnlyCollection<Clan>> FetchClansAsync();

        Task<Clan?> FindClanAsync(ClanId clanId);

        Task<bool> ExistsAsync(string name);

        Task<Stream> DownloadLogoAsync(ClanId clanId);

        Task UploadLogoAsync(ClanId clanId, IFormFile logo);

        Task DeleteLogoAsync(ClanId clanId);

        Task<IReadOnlyCollection<Member>> FetchMembersAsync(ClanId clanId);

        Task<Member?> FindMemberAsync(ClanId clanId, MemberId memberId);

        Task<bool> IsMemberAsync(UserId userId);

        Task<bool> IsOwnerAsync(ClanId clanId, UserId ownerId);
    }
}
