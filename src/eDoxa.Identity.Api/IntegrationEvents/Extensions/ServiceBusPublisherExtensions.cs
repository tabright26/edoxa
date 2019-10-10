// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Extensions
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

        public static async Task PublishUserCreatedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string email,
            string country
        )
        {
            await publisher.PublishAsync(new UserCreatedIntegrationEvent(userId, email, country));
        }
    }
}
