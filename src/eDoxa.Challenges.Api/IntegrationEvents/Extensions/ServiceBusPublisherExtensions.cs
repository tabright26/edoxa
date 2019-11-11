﻿// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishTransactionSuccededIntegrationEventAsync(this IServiceBusPublisher publisher, IDictionary<string, string> metadata)
        {
            await publisher.PublishAsync(new TransactionSuccededIntegrationEvent(metadata));
        }

        public static async Task PublishUserEmailSentIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string subject,
            string htmlMessage
        )
        {
            await publisher.PublishAsync(new UserEmailSentIntegrationEvent(userId, subject, htmlMessage));
        }
    }
}
