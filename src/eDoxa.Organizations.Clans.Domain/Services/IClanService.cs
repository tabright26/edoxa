// Filename: IAccountService.cs
// Date Created: 2019-07-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;

using FluentValidation.Results;

namespace eDoxa.Organizations.Clans.Domain.Services
{
    public interface IClanService
    {
        Task<IReadOnlyCollection<Clan>> FetchClansAsync();

        Task<Clan?> FindClanAsync(ClanId clanId);

        Task<ValidationResult> CreateClanAsync(UserId userId, string name);

        Task<FileStream?> GetClanLogoAsync(ClanId clanId);

        Task<ValidationResult> CreateOrUpdateClanLogoAsync(Clan clan, FileStream logo, Guid userId);

        Task<IReadOnlyCollection<Member>> FetchMembersAsync(ClanId clanId);

        Task<Member?> FindMemberAsync(Clan clan, MemberId memberId);

        Task<ValidationResult> AddMemberToClanAsync(Clan clan, IMemberInfo memberInfo);

        Task<ValidationResult> KickMemberFromClanAsync(Clan clan, MemberId memberId);

        Task<ValidationResult> LeaveClanAsync (Clan clan, UserId userId);
    }
}
