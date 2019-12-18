// Filename: ChallengeParticipantRegisteredDomainEventHandler.cs
// Date Created: 2019-11-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

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
            //var metadata = new Dictionary<string, string>
            //{
            //    [nameof(ChallengeId)] = domainEvent.ChallengeId.ToString(),
            //    [nameof(ParticipantId)] = domainEvent.ParticipantId.ToString()
            //};

            //await _serviceBusPublisher.PublishTransactionSuccededIntegrationEventAsync(domainEvent.UserId, metadata);

            await Task.CompletedTask;
        }
    }
}
