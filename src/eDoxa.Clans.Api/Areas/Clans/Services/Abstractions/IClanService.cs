// Filename: IClanService.cs
// Date Created: 2019-10-02
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Api.Areas.Clans.Services.Abstractions
{
    public interface IClanService
    {
        Task<IReadOnlyCollection<Clan>> FetchClansAsync();

        Task<Clan?> FindClanAsync(ClanId clanId);

        Task<DomainValidationResult> CreateClanAsync(UserId userId, string name);

        Task<DomainValidationResult> UpdateClanAsync(Clan clan, UserId userId, string? summary);

        Task<Stream> DownloadLogoAsync(Clan clan);

        Task<DomainValidationResult> UploadLogoAsync(
            Clan clan,
            UserId userId,
            Stream stream,
            string fileName
        );

        Task DeleteLogoAsync(ClanId clanId);

        /// <remarks>
        ///     This method is called by domain event and should never fail. If something fails in the domain event handler,
        ///     an exception must be thrown to inform the developer that something is not working properly.
        /// </remarks>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddMemberToClanAsync(ClanId clanId, IMemberInfo memberInfo);

        Task<IReadOnlyCollection<Member>> FetchMembersAsync(Clan clan);

        Task<Member?> FindMemberAsync(Clan clan, MemberId memberId);

        Task<DomainValidationResult> KickMemberFromClanAsync(Clan clan, UserId userId, MemberId memberId);

        Task<DomainValidationResult> LeaveClanAsync(Clan clan, UserId userId);

        Task<bool> IsMemberAsync(UserId userId);

        Task<IReadOnlyCollection<Division>> FetchDivisionsAsync(ClanId clanId);

        Task<IReadOnlyCollection<Member>> FetchDivisionMembersAsync(DivisionId divisionId);

        Task<DomainValidationResult> CreateDivisionAsync(Clan clan, UserId userId, string name, string description);

        Task<DomainValidationResult> DeleteDivisionAsync(Clan clan, UserId userId, DivisionId divisionId);

        Task<DomainValidationResult> UpdateDivisionAsync(
            Clan clan,
            UserId userId,
            DivisionId divisionId,
            string name,
            string description
        );

        Task<DomainValidationResult> AddMemberToDivisionAsync(Clan clan, UserId userId, DivisionId divisionId, MemberId memberId);

        Task<DomainValidationResult> RemoveMemberFromDivisionAsync(Clan clan, UserId userId, DivisionId divisionId, MemberId memberId);
    }
}
