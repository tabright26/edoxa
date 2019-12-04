// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserTransactionEmailSentIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            ITransaction transaction
        )
        {
            await publisher.PublishAsync(
                new UserEmailSentIntegrationEvent(
                    userId,
                    $"{transaction.GetType().Name} - {transaction.Currency.Type} - {transaction.Status.Name}",
                    transaction.Description.Text));
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
