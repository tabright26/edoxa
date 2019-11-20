// Filename: ChallengeClosedDomainEventHandler.cs
// Date Created: 2019-11-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.IntegrationEvents.Extensions;
using eDoxa.Challenges.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.DomainEvents.Handlers
{
    public sealed class ChallengeClosedDomainEventHandler : IDomainEventHandler<ChallengeClosedDomainEvent>
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengeClosedDomainEventHandler(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task Handle(ChallengeClosedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _serviceBusPublisher.PublishChallengeClosedIntegrationEventAsync(domainEvent.Challenge.Id, domainEvent.Challenge.Scoreboard);
        }
    }
}
