// Filename: IAccountService.cs
// Date Created: 2019-07-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;

using FluentValidation.Results;

using Microsoft.AspNetCore.Http;

namespace eDoxa.Organizations.Clans.Domain.Services
{
    public interface IClanService
    {
        Task<IReadOnlyCollection<Clan>> FetchClansAsync();

        Task<Clan?> FindClanAsync(ClanId clanId);

        Task<ValidationResult> CreateClanAsync(UserId userId, string name);

        Task<Stream> DownloadLogoAsync(Clan clan);

        Task<ValidationResult> UploadLogoAsync(Clan clan, UserId userId, IFormFile logo);

        Task DeleteLogoAsync(ClanId clanId);

        Task AddMemberToClanAsync(ClanId clanId, IMemberInfo memberInfo);

        Task<IReadOnlyCollection<Member>> FetchMembersAsync(Clan clan);

        Task<Member?> FindMemberAsync(Clan clan, MemberId memberId);

        Task<ValidationResult> KickMemberFromClanAsync(UserId userId, Clan clan, MemberId memberId);

        Task<ValidationResult> LeaveClanAsync (Clan clan, UserId userId);

        Task<bool> IsMemberAsync(UserId userId);
    }
}
