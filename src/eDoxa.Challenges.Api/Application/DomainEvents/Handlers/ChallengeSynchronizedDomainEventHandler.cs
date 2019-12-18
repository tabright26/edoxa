// Filename: ChallengeSynchronizedDomainEventHandler.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.IntegrationEvents.Extensions;
using eDoxa.Challenges.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.Application.DomainEvents.Handlers
{
    public sealed class ChallengeSynchronizedDomainEventHandler : IDomainEventHandler<ChallengeSynchronizedDomainEvent>
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengeSynchronizedDomainEventHandler(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task Handle(ChallengeSynchronizedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _serviceBusPublisher.PublishChallengeSynchronizedIntegrationEventAsync(domainEvent.Challenge);
        }
    }
}
