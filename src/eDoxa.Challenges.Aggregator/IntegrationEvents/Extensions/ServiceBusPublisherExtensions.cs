// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Aggregator.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishParticipantRegistrationFailedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            ChallengeId challengeId,
            ParticipantId participantId
        )
        {
            await publisher.PublishAsync(new ParticipantRegistrationFailedIntegrationEvent(challengeId, participantId));
        }

        public static async Task PublishChallengeCreationFailedIntegrationEventAsync(this IServiceBusPublisher publisher, ChallengeId challengeId)
        {
            await publisher.PublishAsync(new ChallengeCreationFailedIntegrationEvent(challengeId));
        }
    }
}
