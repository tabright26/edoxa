// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Application.Profiles;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishChallengeStartedIntegrationEventAsync(this IServiceBusPublisher publisher, IChallenge challenge)
        {
            var integrationEvent = new ChallengeStartedIntegrationEvent
            {
                Challenge = ChallengeProfile.Map(challenge)
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishChallengeSynchronizedIntegrationEventAsync(this IServiceBusPublisher publisher, IChallenge challenge)
        {
            var integrationEvent = new ChallengeSynchronizedIntegrationEvent
            {
                ChallengeId = challenge.Id,
                Scoreboard =
                {
                    challenge.Participants.Select(participant => ChallengeProfile.Map(challenge, participant))
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishChallengeParticipantRegisteredIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            ChallengeId challengeId,
            UserId userId,
            ParticipantId participantId
        )
        {
            var integrationEvent = new ChallengeParticipantRegisteredIntegrationEvent
            {
                Participant = new ParticipantDto
                {
                    Id = participantId,
                    ChallengeId = challengeId,
                    UserId = userId
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishRegisterChallengeParticipantFailedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            ChallengeId challengeId,
            UserId userId,
            ParticipantId participantId
        )
        {
            var integrationEvent = new RegisterChallengeParticipantFailedIntegrationEvent
            {
                Participant = new ParticipantDto
                {
                    Id = participantId,
                    ChallengeId = challengeId,
                    UserId = userId
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }
    }
}
