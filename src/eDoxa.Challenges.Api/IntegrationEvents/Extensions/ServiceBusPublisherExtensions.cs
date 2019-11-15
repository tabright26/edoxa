// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishChallengeClosedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            ChallengeId challengeId,
            IScoreboard scoreboard
        )
        {
            await publisher.PublishAsync(
                new ChallengeClosedIntegrationEvent(challengeId, scoreboard.ToDictionary(item => item.Key, item => item.Value?.ToDecimal())));
        }

        public static async Task PublishTransactionSuccededIntegrationEventAsync(this IServiceBusPublisher publisher, IDictionary<string, string> metadata)
        {
            await publisher.PublishAsync(new TransactionSuccededIntegrationEvent(metadata));
        }

        public static async Task PublishUserEmailSentIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string subject,
            string htmlMessage
        )
        {
            await publisher.PublishAsync(new UserEmailSentIntegrationEvent(userId, subject, htmlMessage));
        }
    }
}
