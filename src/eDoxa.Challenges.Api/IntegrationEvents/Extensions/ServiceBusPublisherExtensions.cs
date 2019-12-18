// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.CustomTypes;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishCreateChallengeFailedIntegrationEventAsync(this IServiceBusPublisher publisher, ChallengeId challengeId)
        {
            var integrationEvent = new CreateChallengeFailedIntegrationEvent
            {
                ChallengeId = challengeId
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishChallengeSynchronizedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            ChallengeId challengeId,
            IScoreboard scoreboard
        )
        {
            var integrationEvent = new ChallengeSynchronizedIntegrationEvent
            {
                ChallengeId = challengeId,
                Scoreboard =
                {
                    scoreboard.ToDictionary(item => item.Key.ToString(), item => DecimalValue.FromDecimal(item.Value?.ToDecimal() ?? 0M)) // TODO
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }
    }
}
