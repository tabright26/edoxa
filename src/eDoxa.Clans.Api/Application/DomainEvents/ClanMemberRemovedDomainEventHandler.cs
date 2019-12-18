// Filename: ClanMemberRemovedDomainEventHandler.cs
// Date Created: 2019-10-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Clans.Api.IntegrationEvents.Extensions;
using eDoxa.Clans.Domain.DomainEvents;
using eDoxa.Clans.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Clans.Api.Application.DomainEvents
{
    public sealed class ClanMemberRemovedDomainEventHandler : IDomainEventHandler<ClanMemberRemovedDomainEvent>
    {
        private readonly IClanService _clanService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ClanMemberRemovedDomainEventHandler(IClanService clanService, IServiceBusPublisher serviceBusPublisher)
        {
            _clanService = clanService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task Handle(ClanMemberRemovedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var clan = await _clanService.FindClanAsync(domainEvent.ClanId);

            await _serviceBusPublisher.PublishClanMemberRemovedIntegrationEventAsync(domainEvent.UserId, clan!);
        }
    }
}
