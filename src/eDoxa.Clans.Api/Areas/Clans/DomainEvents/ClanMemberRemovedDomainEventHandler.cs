// Filename: ClanMemberRemovedDomainEventHandler.cs
// Date Created: 2019-10-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.IntegrationEvents.Extensions;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Seedwork.Domain;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Clans.Api.Areas.Clans.DomainEvents
{
    // GABRIEL: UNIT TEST.
    public sealed class ClanMemberRemovedDomainEventHandler : IDomainEventHandler<ClanMemberRemovedDomainEvent>
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ClanMemberRemovedDomainEventHandler(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task Handle(ClanMemberRemovedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _serviceBusPublisher.PublishUserClaimClanIdRemovedIntegrationEventAsync(domainEvent.UserId, domainEvent.ClanId);
        }
    }
}
