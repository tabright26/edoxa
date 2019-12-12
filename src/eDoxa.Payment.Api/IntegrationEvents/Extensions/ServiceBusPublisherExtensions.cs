// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
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

        public static async Task PublishUserTransactionFailedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionId transactionId
        )
        {
            await publisher.PublishAsync(
                new UserTransactionFailedIntegrationEvent
                {
                    UserId = userId,
                    TransactionId = transactionId
                });
        }

        public static async Task PublishUserTransactionSuccededIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionId transactionId
        )
        {
            await publisher.PublishAsync(
                new UserTransactionSuccededIntegrationEvent
                {
                    UserId = userId,
                    TransactionId = transactionId
                });
        }
    }
}
