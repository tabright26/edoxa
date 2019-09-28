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
using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;

using FluentValidation.Results;

namespace eDoxa.Organizations.Clans.Domain.Services
{
    public interface IInvitationService
    {
        Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(ClanId clanId);

        Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(UserId userId);

        Task<Invitation?> FindInvitationAsync(InvitationId invitationId);

        Task<ValidationResult> SendInvitationAsync(UserId recruiterId, ClanId recruiterClan, UserId inviteId);

        Task<ValidationResult> AcceptInvitationAsync(UserId userId, Invitation invitation);

        Task<ValidationResult> DeclineInvitationAsync(Invitation invitation);

        Task<Clan?> FindClanAsync(ClanId clanId);
    }
}
