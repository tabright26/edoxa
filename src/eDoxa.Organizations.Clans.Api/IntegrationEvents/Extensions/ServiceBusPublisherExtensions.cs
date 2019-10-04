﻿// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Domain.Models;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Organizations.Clans.Api.IntegrationEvents.Extensions
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
    }
}
