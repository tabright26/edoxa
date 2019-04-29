// Filename: ChallengeUserPrizesSnapshottedDomainEventHandler.cs
// Date Created: 2019-04-14
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
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.DomainEvent;
using eDoxa.Seedwork.Application.DomainEventHandlers;
using eDoxa.ServiceBus;

using JetBrains.Annotations;

namespace eDoxa.Challenges.Application.DomainEventHandlers
{
    internal sealed class ChallengePaidOffDomainEventHandler : IDomainEventHandler<ChallengePaidOffDomainEvent>
    {
        private readonly IIntegrationEventService _integrationEventService;

        public ChallengePaidOffDomainEventHandler(IIntegrationEventService integrationEventService)
        {
            _integrationEventService = integrationEventService;
        }

        public async Task Handle([NotNull] ChallengePaidOffDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var integrationEvent = new ChallengeUserPrizesSnapshottedIntegrationEvent(domainEvent.ChallengeId.ToGuid(), domainEvent.Payoff.ToDictionary(userPrize => userPrize.Key.ToGuid(), userPrize => (decimal) userPrize.Value));

            await _integrationEventService.PublishAsync(integrationEvent);
        }
    }
}