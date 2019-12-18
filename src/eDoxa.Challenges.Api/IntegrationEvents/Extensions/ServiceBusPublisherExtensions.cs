// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Grpc.Protos.Challenges.Dtos;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishChallengeSynchronizedIntegrationEventAsync(this IServiceBusPublisher publisher, IChallenge challenge)
        {
            var integrationEvent = new ChallengeSynchronizedIntegrationEvent
            {
                ChallengeId = challenge.Id,
                Scoreboard =
                {
                    challenge.Scoreboard.ToDictionary(
                        item => item.Key.ToString(),
                        item => item.Value == null ? null : DecimalValue.FromDecimal(item.Value.ToDecimal()))
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
