// Filename: CredentialCreatedDomainEventHandler.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Arena.Games.Api.IntegrationEvents.Extensions;
using eDoxa.Arena.Games.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Arena.Games.Api.DomainEvents.Handlers
{
    public sealed class CredentialCreatedDomainEventHandler : IDomainEventHandler<CredentialCreatedDomainEvent>
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public CredentialCreatedDomainEventHandler(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task Handle(CredentialCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _serviceBusPublisher.PublishUserGamePlayerIdClaimAddedIntegrationEvent(domainEvent.Credential);
        }
    }
}
