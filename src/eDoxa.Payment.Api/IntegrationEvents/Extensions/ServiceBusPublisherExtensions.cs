// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

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

        public static async Task PublishUserTransactionFailedIntegrationEventAsync(this IServiceBusPublisher publisher, TransactionId transactionId)
        {
            await publisher.PublishAsync(new UserTransactionFailedIntegrationEvent(transactionId));
        }

        public static async Task PublishUserTransactionSuccededIntegrationEventAsync(this IServiceBusPublisher publisher, TransactionId transactionId)
        {
            await publisher.PublishAsync(new UserTransactionSuccededIntegrationEvent(transactionId));
        }
    }
}
