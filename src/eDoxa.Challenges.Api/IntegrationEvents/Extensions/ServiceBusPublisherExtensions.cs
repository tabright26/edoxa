// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
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
                new ChallengeClosedIntegrationEvent
                {
                    ChallengeId = challengeId,
                    Scoreboard =
                    {
                        scoreboard.ToDictionary(item => item.Key.ToString(), item => Convert.ToDouble(item.Value?.ToDecimal() ?? 0))
                    }
                });
        }

        public static async Task PublishTransactionSuccededIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            IDictionary<string, string> metadata
        )
        {
            await publisher.PublishAsync(
                new TransactionSuccededIntegrationEvent
                {
                    UserId = userId,
                    Metadata =
                    {
                        metadata
                    }
                });
        }

        public static async Task PublishUserEmailSentIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string subject,
            string htmlMessage
        )
        {
            await publisher.PublishAsync(
                new UserEmailSentIntegrationEvent
                {
                    UserId = userId,
                    Subject = subject,
                    HtmlMessage = htmlMessage
                });
        }
    }
}
