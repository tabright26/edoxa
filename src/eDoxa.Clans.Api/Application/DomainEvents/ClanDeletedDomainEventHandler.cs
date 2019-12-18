// Filename: ClanDeletedDomainEventHandler.cs
// Date Created: 2019-10-01
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Logging;

namespace eDoxa.Clans.Api.Application.DomainEvents
{
    public sealed class ClanDeletedDomainEventHandler : IDomainEventHandler<ClanDeletedDomainEvent>
    {
        private readonly IClanService _clanService;
        private readonly ICandidatureService _candidatureService;
        private readonly IInvitationService _invitationService;
        private readonly ILogger _logger;

        public ClanDeletedDomainEventHandler(IClanService clanService, ICandidatureService candidatureService, IInvitationService invitationService, ILogger<ClanDeletedDomainEventHandler> logger)
        {
            _clanService = clanService;
            _candidatureService = candidatureService;
            _invitationService = invitationService;
            _logger = logger;
        }

        public async Task Handle(ClanDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _clanService.DeleteLogoAsync(domainEvent.ClanId);

            await this.DeleteCandidaturesAsync(domainEvent.ClanId);

            await this.DeleteInvitationsAsync(domainEvent.ClanId);
        }

        private async Task DeleteCandidaturesAsync(ClanId clanId)
        {
            try
            {
                await _invitationService.DeleteInvitationsAsync(clanId);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "The clan's user invitations have not been deleted correctly.");
            }
        }

        private async Task DeleteInvitationsAsync(ClanId clanId)
        {
            try
            {
                await _candidatureService.DeleteCandidaturesAsync(clanId);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "The clan's user candidatures have not been deleted correctly.");
            }
        }
    }
}
