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
using eDoxa.Seedwork.Domain.Miscs;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Clans.Api.Areas.Clans.Services.Abstractions
{
    public interface IClanService
    {
        Task<IReadOnlyCollection<Clan>> FetchClansAsync();

        Task<Clan?> FindClanAsync(ClanId clanId);

        Task<ValidationResult> CreateClanAsync(UserId userId, string name);

        Task<ValidationResult> UpdateClanAsync(Clan clan, UserId userId, string summary);

        Task<Stream> DownloadLogoAsync(Clan clan);

        Task<ValidationResult> UploadLogoAsync(Clan clan, UserId userId, IFormFile logo);

        Task DeleteLogoAsync(ClanId clanId);

        /// <remarks>
        ///     This method is called by domain event and should never fail. If something fails in the domain event handler,
        ///     an exception must be thrown to inform the developer that something is not working properly.
        /// </remarks>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddMemberToClanAsync(ClanId clanId, IMemberInfo memberInfo);

        Task<IReadOnlyCollection<Member>> FetchMembersAsync(Clan clan);

        Task<Member?> FindMemberAsync(Clan clan, MemberId memberId);

        Task<ValidationResult> KickMemberFromClanAsync(Clan clan, UserId userId, MemberId memberId);

        Task<ValidationResult> LeaveClanAsync(Clan clan, UserId userId);

        Task<bool> IsMemberAsync(UserId userId);

        Task<IReadOnlyCollection<Division>> FetchDivisionsAsync(ClanId clanId);

        Task<IReadOnlyCollection<Member>> FetchDivisionMembersAsync(DivisionId divisionId);

        Task<ValidationResult> CreateDivisionAsync(Clan clan, UserId userId, string name, string description);

        Task<ValidationResult> DeleteDivisionAsync(Clan clan, UserId userId, DivisionId divisionId);

        Task<ValidationResult> UpdateDivisionAsync(
            Clan clan,
            UserId userId,
            DivisionId divisionId,
            string name,
            string description
        );

        Task<ValidationResult> AddMemberToDivisionAsync(Clan clan, UserId userId, DivisionId divisionId, MemberId memberId);

        Task<ValidationResult> RemoveMemberFromDivisionAsync(Clan clan, UserId userId, DivisionId divisionId, MemberId memberId);
    }
}
