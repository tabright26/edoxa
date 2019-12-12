// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Web.Aggregator.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishTransactionCanceledIntegrationEventAsync(this IServiceBusPublisher publisher, TransactionId transactionId)
        {
            await publisher.PublishAsync(
                new TransactionCanceledIntegrationEvent
                {
                    TransactionId = transactionId
                });
        }

        public static async Task PublishChallengeDeletedIntegrationEventAsync(this IServiceBusPublisher publisher, ChallengeId challengeId)
        {
            await publisher.PublishAsync(
                new ChallengeDeletedIntegrationEvent
                {
                    ChallengeId = challengeId
                });
        }
    }
}
