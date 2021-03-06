﻿// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserStripeCustomerCreatedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, string customer)
        {
            var integrationEvent = new UserStripeCustomerCreatedIntegrationEvent
            {
                UserId = userId,
                Customer = customer
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserStripePaymentIntentCanceledIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionId transactionId
        )
        {
            var integrationEvent = new UserStripePaymentIntentCanceledIntegrationEvent
            {
                UserId = userId,
                TransactionId = transactionId
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserStripePaymentIntentSucceededIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionId transactionId
        )
        {
            var integrationEvent = new UserStripePaymentIntentSucceededIntegrationEvent
            {
                UserId = userId,
                TransactionId = transactionId
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserStripePaymentIntentPaymentFailedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionId transactionId
        )
        {
            var integrationEvent = new UserStripePaymentIntentPaymentFailedIntegrationEvent
            {
                UserId = userId,
                TransactionId = transactionId
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserWithdrawSucceededIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionDto transaction
        )
        {
            var integrationEvent = new UserWithdrawSucceededIntegrationEvent
            {
                UserId = userId,
                Transaction = transaction
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserWithdrawFailedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            TransactionDto transaction
        )
        {
            var integrationEvent = new UserWithdrawFailedIntegrationEvent
            {
                UserId = userId,
                Transaction = transaction
            };

            await publisher.PublishAsync(integrationEvent);
        }
    }
}
