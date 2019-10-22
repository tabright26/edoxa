// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Seedwork.Domain.Miscs;
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
            await publisher.PublishAsync(new UserEmailSentIntegrationEvent(userId, subject, htmlMessage));
        }

        public static async Task PublishUserTransactionFailedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, TransactionId transactionId)
        {
            await publisher.PublishAsync(new UserTransactionFailedIntegrationEvent(userId, transactionId));
        }

        public static async Task PublishUserTransactionSuccededIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, TransactionId transactionId)
        {
            await publisher.PublishAsync(new UserTransactionSuccededIntegrationEvent(userId, transactionId));
        }
    }
}
