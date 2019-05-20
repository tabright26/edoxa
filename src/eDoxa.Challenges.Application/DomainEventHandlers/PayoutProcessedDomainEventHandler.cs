// Filename: PayoutProcessedDomainEventHandler.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Application.IntegrationEvents;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvents;
using eDoxa.Seedwork.Application.DomainEventHandlers;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Application.DomainEventHandlers
{
    internal sealed class PayoutProcessedDomainEventHandler : IDomainEventHandler<PayoutProcessedDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;

        public PayoutProcessedDomainEventHandler(IIntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService;
        }

        public async Task Handle([NotNull] PayoutProcessedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new ChallengePayoutProcessedIntegrationEvent(domainEvent.ChallengeId.ToGuid(),
                domainEvent.ParticipantPrizes.ToDictionary(userPrize => userPrize.Key.ToGuid(), userPrize => (decimal) userPrize.Value));

            await _integrationEventService.PublishAsync(integrationEvent);
        }
    }
}