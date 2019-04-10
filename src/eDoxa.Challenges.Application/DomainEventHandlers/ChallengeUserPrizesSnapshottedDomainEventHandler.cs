// Filename: ChallengeUserPrizesSnapshottedDomainEventHandler.cs
// Date Created: 2019-03-21
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.IntegrationEvents;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvent;
using eDoxa.Seedwork.Application.DomainEventHandlers;
using eDoxa.ServiceBus;

namespace eDoxa.Challenges.Application.DomainEventHandlers
{
    public sealed class ChallengeUserPrizesSnapshottedDomainEventHandler : IDomainEventHandler<ChallengeUserPrizesSnapshottedDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;

        public ChallengeUserPrizesSnapshottedDomainEventHandler(IIntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService ?? throw new ArgumentNullException(nameof(integrationEventService));
        }

        public async Task Handle(ChallengeUserPrizesSnapshottedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new ChallengeUserPrizesSnapshottedIntegrationEvent(domainEvent.ChallengeId, domainEvent.UserPrizes);

            await _integrationEventService.PublishAsync(integrationEvent);
        }
    }
}