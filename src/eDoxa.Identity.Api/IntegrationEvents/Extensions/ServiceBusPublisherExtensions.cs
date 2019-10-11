// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
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

        public static async Task PublishUserAddressChangedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string line1,
            string? line2,
            string? state,
            string city,
            string postalCode
        )
        {
            await publisher.PublishAsync(
                new UserAddressChangedIntegrationEvent(
                    userId,
                    line1,
                    line2,
                    state,
                    city,
                    postalCode));
        }

        public static async Task PublishUserEmailChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, string email)
        {
            await publisher.PublishAsync(new UserEmailChangedIntegrationEvent(userId, email));
        }

        public static async Task PublishUserInformationChangedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string firstName,
            string lastName,
            string gender,
            DateTime dob
        )
        {
            await publisher.PublishAsync(
                new UserInformationChangedIntegrationEvent(
                    userId,
                    firstName,
                    lastName,
                    gender,
                    dob));
        }

        public static async Task PublishUserPhoneChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, string phoneNumber)
        {
            await publisher.PublishAsync(new UserPhoneChangedIntegrationEvent(userId, phoneNumber));
        }
    }
}
