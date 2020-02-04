// Filename: IClanService.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Domain.Services
{
    public interface IClanService
    {
        Task<IReadOnlyCollection<Clan>> FetchClansAsync();

        Task<Clan?> FindClanAsync(ClanId clanId);

        Task<DomainValidationResult<Clan>> CreateClanAsync(UserId userId, string name);

        Task<DomainValidationResult<Clan>> UpdateClanAsync(Clan clan, UserId userId, string? summary);

        Task<Stream> DownloadLogoAsync(Clan clan);

        Task<DomainValidationResult<object>> UploadLogoAsync(
            Clan clan,
            UserId userId,
            Stream stream,
            string fileName
        );

        Task DeleteLogoAsync(ClanId clanId);

        Task AddMemberToClanAsync(ClanId clanId, IMemberInfo memberInfo);

        Task<IReadOnlyCollection<Member>> FetchMembersAsync(Clan clan);

        Task<Member?> FindMemberAsync(Clan clan, MemberId memberId);

        Task<DomainValidationResult<Member>> KickMemberFromClanAsync(Clan clan, UserId userId, MemberId memberId);

        Task<DomainValidationResult<Clan>> LeaveClanAsync(Clan clan, UserId userId);

        Task<bool> IsMemberAsync(UserId userId);

        Task<IReadOnlyCollection<Division>> FetchDivisionsAsync(ClanId clanId);

        Task<IReadOnlyCollection<Member>> FetchDivisionMembersAsync(DivisionId divisionId);

        Task<DomainValidationResult<Division>> CreateDivisionAsync(Clan clan, UserId userId, string name, string description);

        Task<DomainValidationResult<Division>> DeleteDivisionAsync(Clan clan, UserId userId, DivisionId divisionId);

        Task<DomainValidationResult<Division>> UpdateDivisionAsync(
            Clan clan,
            UserId userId,
            DivisionId divisionId,
            string name,
            string description
        );

        Task<DomainValidationResult<Member>> AddMemberToDivisionAsync(Clan clan, UserId userId, DivisionId divisionId, MemberId memberId);

        Task<DomainValidationResult<Member>> RemoveMemberFromDivisionAsync(Clan clan, UserId userId, DivisionId divisionId, MemberId memberId);
    }
}
