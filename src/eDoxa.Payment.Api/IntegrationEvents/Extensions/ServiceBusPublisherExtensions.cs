// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserDepositSucceededIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionDto transaction
        )
        {
            var integrationEvent = new UserDepositSucceededIntegrationEvent
            {
                UserId = userId,
                Transaction = transaction
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserDepositFailedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, TransactionDto transaction)
        {
            var integrationEvent = new UserDepositFailedIntegrationEvent
            {
                UserId = userId,
                Transaction = transaction
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserWithdrawalSucceededIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionDto transaction
        )
        {
            var integrationEvent = new UserWithdrawalSucceededIntegrationEvent
            {
                UserId = userId,
                Transaction = transaction
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserWithdrawalFailedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionDto transaction
        )
        {
            var integrationEvent = new UserWithdrawalFailedIntegrationEvent
            {
                UserId = userId,
                Transaction = transaction
            };

            await publisher.PublishAsync(integrationEvent);
        }
    }
}
