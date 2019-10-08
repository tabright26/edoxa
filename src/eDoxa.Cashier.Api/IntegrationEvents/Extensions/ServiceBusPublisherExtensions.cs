// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Extensions
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
