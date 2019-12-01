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

using eDoxa.Clans.Domain.Models;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Clans.Api.Areas.Clans.Services.Abstractions
{
    public interface IInvitationService
    {
        Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(ClanId clanId);

        Task<IReadOnlyCollection<Invitation>> FetchInvitationsAsync(UserId userId);

        Task<Invitation?> FindInvitationAsync(InvitationId invitationId);

        Task<DomainValidationResult> SendInvitationAsync(ClanId clanId, UserId userId, UserId ownerId);

        Task<DomainValidationResult> AcceptInvitationAsync(Invitation invitation, UserId userId);

        Task<DomainValidationResult> DeclineInvitationAsync(Invitation invitation, UserId userId);

        Task DeleteInvitationsAsync(ClanId clanId);

        Task DeleteInvitationsAsync(UserId userId);
    }
}
