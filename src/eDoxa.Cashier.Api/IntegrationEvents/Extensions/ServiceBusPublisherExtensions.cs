// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
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
            Guid transactionId,
            string transactionDescription,
            string customerId,
            long amount
        )
        {
            await publisher.PublishAsync(
                new UserAccountDepositIntegrationEvent(
                    transactionId,
                    transactionDescription,
                    customerId,
                    amount));
        }

        public static async Task PublishUserAccountWithdrawalIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            Guid transactionId,
            string transactionDescription,
            string connectAccountId,
            long amount
        )
        {
            await publisher.PublishAsync(
                new UserAccountWithdrawalIntegrationEvent(
                    transactionId,
                    transactionDescription,
                    connectAccountId,
                    amount));
        }
    }
}
