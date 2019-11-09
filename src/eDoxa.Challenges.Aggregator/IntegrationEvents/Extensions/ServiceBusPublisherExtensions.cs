// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Aggregator.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishTransactionCanceledIntegrationEventAsync(this IServiceBusPublisher publisher, IDictionary<string, string> metadata)
        {
            await publisher.PublishAsync(new TransactionCanceledIntegrationEvent(metadata));
        }

        public static async Task PublishChallengeCreationFailedIntegrationEventAsync(this IServiceBusPublisher publisher, ChallengeId challengeId)
        {
            await publisher.PublishAsync(new ChallengeCreationFailedIntegrationEvent(challengeId));
        }
    }
}
