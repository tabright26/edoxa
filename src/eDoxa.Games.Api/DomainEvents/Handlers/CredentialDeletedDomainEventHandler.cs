// Filename: CredentialDeletedDomainEventHandler.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Games.Api.IntegrationEvents.Extensions;
using eDoxa.Games.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Games.Api.DomainEvents.Handlers
{
    public sealed class CredentialDeletedDomainEventHandler : IDomainEventHandler<CredentialDeletedDomainEvent>
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public CredentialDeletedDomainEventHandler(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task Handle(CredentialDeletedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _serviceBusPublisher.PublishUserGamePlayerIdClaimRemovedIntegrationEvent(domainEvent.Credential);
        }
    }
}
