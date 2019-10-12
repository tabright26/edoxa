// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Payment.Domain.Stripe.Models;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishEmailSentIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string email,
            string subject,
            string htmlMessage
        )
        {
            await publisher.PublishAsync(
                new EmailSentIntegrationEvent(
                    userId,
                    email,
                    subject,
                    htmlMessage));
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
