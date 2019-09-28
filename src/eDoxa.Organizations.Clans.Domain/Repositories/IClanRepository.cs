// Filename: IClanRepository.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;

namespace eDoxa.Organizations.Clans.Domain.Repositories
{
    public interface IClanRepository
    {
        void Create(Clan clan);

        void Delete(Clan clan);

        Task<IReadOnlyCollection<Clan>> FetchClansAsync();

        Task<Clan?> FindClanAsync(ClanId clanId);

        Task<FileStream?> GetLogoAsync(ClanId clanId);

        Task CreateOrUpdateLogoAsync(FileStream logo);

        Task<IReadOnlyCollection<Member>> FetchMembersAsync(ClanId clanId);

        Task<Member?> FindMemberAsync(ClanId clanId, MemberId memberId);

        Task<bool> HasMember(UserId userId);

        Task CommitAsync();
    }
}
