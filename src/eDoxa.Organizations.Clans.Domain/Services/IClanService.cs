﻿// Filename: IClanService.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
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

        /// <remarks>
        ///     This method is called by domain event and should never fail. If something fails in the domain event handler,
        ///     an exception must be thrown to inform the developer that something is not working properly.
        /// </remarks>
        /// <exception cref="InvalidOperationException"></exception>
        Task AddMemberToClanAsync(ClanId clanId, IMemberInfo memberInfo);

        Task<IReadOnlyCollection<Member>> FetchMembersAsync(Clan clan);

        Task<Member?> FindMemberAsync(Clan clan, MemberId memberId);

        Task<ValidationResult> KickMemberFromClanAsync(UserId userId, Clan clan, MemberId memberId);

        Task<ValidationResult> LeaveClanAsync(Clan clan, UserId userId);

        Task<bool> IsMemberAsync(UserId userId);
    }
}