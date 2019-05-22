// Filename: ChallengePayoutDomainEventHandler.cs
// Date Created: 2019-05-20
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

using eDoxa.Arena.Challenges.Application.IntegrationEvents;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvents;
using eDoxa.Seedwork.Application.DomainEventHandlers;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Arena.Challenges.Application.DomainEventHandlers
{
    internal sealed class ChallengePayoutDomainEventHandler : IDomainEventHandler<ChallengePayoutDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;

        public ChallengePayoutDomainEventHandler(IIntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService;
        }

        public async Task Handle([NotNull] ChallengePayoutDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _integrationEventService.PublishAsync(
                new ChallengePayoutIntegrationEvent(
                    domainEvent.ChallengeId.ToGuid(),
                    domainEvent.ParticipantPrizes.ToDictionary(
                        userPrize => userPrize.Key.ToGuid(),
                        userPrize => userPrize.Value != null ? (decimal?) userPrize.Value : null
                    )
                )
            );
        }
    }
}
