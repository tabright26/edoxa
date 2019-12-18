// Filename: ChallengeParticipantRegisteredDomainEventHandler.cs
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
    public sealed class ChallengeParticipantRegisteredDomainEventHandler : IDomainEventHandler<ChallengeParticipantRegisteredDomainEvent>
    {
        private readonly IServiceBusPublisher _serviceBusPublisher;

        public ChallengeParticipantRegisteredDomainEventHandler(IServiceBusPublisher serviceBusPublisher)
        {
            _serviceBusPublisher = serviceBusPublisher;
        }

        public async Task Handle(ChallengeParticipantRegisteredDomainEvent domainEvent, CancellationToken cancellationToken)
        {
            await _serviceBusPublisher.PublishChallengeParticipantRegisteredIntegrationEventAsync(
                domainEvent.ChallengeId,
                domainEvent.UserId,
                domainEvent.ParticipantId);
        }
    }
}
