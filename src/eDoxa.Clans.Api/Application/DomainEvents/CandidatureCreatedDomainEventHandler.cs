// Filename: CandidatureCreatedDomainEventHandler.cs
// Date Created: 2019-10-24
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
    public sealed class CandidatureCreatedDomainEventHandler : IDomainEventHandler<CandidatureCreatedDomainEvent>
    {
        private readonly IClanService _clanService;
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public CandidatureCreatedDomainEventHandler(IClanService clanService, IServiceBusPublisher serviceBusPublisher)
        {
            _clanService = clanService;
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task Handle(CandidatureCreatedDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            var clan = await _clanService.FindClanAsync(domainEvent.Candidature.ClanId);

            await _serviceBusPublisher.PublishClanCandidatureSentIntegrationEventAsync(domainEvent.Candidature.UserId, clan!);
        }
    }
}
