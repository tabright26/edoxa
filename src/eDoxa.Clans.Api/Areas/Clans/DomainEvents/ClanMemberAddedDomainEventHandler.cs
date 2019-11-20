// Filename: ClanMemberAddedDomainEventHandler.cs
// Date Created: 2019-10-06
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.Areas.Clans.Services.Abstractions;
using eDoxa.Clans.Api.IntegrationEvents.Extensions;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Clans.Api.Areas.Clans.DomainEvents
{
    public sealed class ClanMemberAddedDomainEventHandler : IDomainEventHandler<ClanMemberAddedDomainEvent>
    {
        private readonly IInvitationService _invitationService;
        private readonly ICandidatureService _candidatureService;
        private readonly IServiceBusPublisher _serviceBusPublisher;
        private readonly ILogger _logger;

        public ClanMemberAddedDomainEventHandler(
            IInvitationService invitationService,
            ICandidatureService candidatureService,
            IServiceBusPublisher serviceBusPublisher,
            ILogger<ClanMemberAddedDomainEventHandler> logger
        )
        {
            _invitationService = invitationService;
            _candidatureService = candidatureService;
            _serviceBusPublisher = serviceBusPublisher;
            _logger = logger;
        }

        public async Task Handle(ClanMemberAddedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _serviceBusPublisher.PublishUserClaimClanIdAddedIntegrationEventAsync(domainEvent.UserId, domainEvent.ClanId);

            await this.DeleteInvitationsAsync(domainEvent.UserId);

            await this.DeleteCandidaturesAsync(domainEvent.UserId);
        }

        private async Task DeleteInvitationsAsync(UserId userId)
        {
            try
            {
                await _invitationService.DeleteInvitationsAsync(userId);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "The user's clan invitations have not been deleted correctly.");
            }
        }

        private async Task DeleteCandidaturesAsync(UserId userId)
        {
            try
            {
                await _candidatureService.DeleteCandidaturesAsync(userId);
            }
            catch (Exception exception)
            {
                _logger.LogCritical(exception, "The user's clan candidatures have not been deleted correctly.");
            }
        }
    }
}
