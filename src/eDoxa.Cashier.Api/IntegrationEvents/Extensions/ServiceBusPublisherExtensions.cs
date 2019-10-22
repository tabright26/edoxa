// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Extensions
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

        public static async Task PublishUserAccountDepositIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string email,
            TransactionId transactionId,
            string description,
            long amount
        )
        {
            await publisher.PublishAsync(
                new UserAccountDepositIntegrationEvent(
                    userId,
                    email,
                    transactionId,
                    description,
                    amount));
        }

        public static async Task PublishUserAccountWithdrawalIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string email,
            TransactionId transactionId,
            string description,
            long amount
        )
        {
            await publisher.PublishAsync(
                new UserAccountWithdrawalIntegrationEvent(
                    userId,
                    email,
                    transactionId,
                    description,
                    amount));
        }
    }
}
