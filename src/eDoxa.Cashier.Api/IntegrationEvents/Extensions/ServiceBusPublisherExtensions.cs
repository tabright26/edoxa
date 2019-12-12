// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
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
                new UserEmailSentIntegrationEvent
                {
                    UserId = userId,
                    Subject = $"{transaction.GetType().Name} - {transaction.Currency.Type} - {transaction.Status.Name}",
                    HtmlMessage = transaction.Description.Text
                });
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
                new UserAccountDepositIntegrationEvent
                {
                    UserId = userId,
                    Email = email,
                    TransactionId = transactionId,
                    Description = description,
                    Amount = amount
                });
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
                new UserAccountWithdrawalIntegrationEvent
                {
                    UserId = userId,
                    Email = email,
                    TransactionId = transactionId,
                    Description = description,
                    Amount = amount
                });
        }
    }
}
