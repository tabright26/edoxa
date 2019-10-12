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

        public static async Task PublishUserAddressChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, UserAddress address)
        {
            await publisher.PublishAsync(
                new UserAddressChangedIntegrationEvent(
                    userId,
                    address.Line1,
                    address.Line2,
                    address.State,
                    address.City,
                    address.PostalCode));
        }

        public static async Task PublishUserEmailChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, string email)
        {
            await publisher.PublishAsync(new UserEmailChangedIntegrationEvent(userId, email));
        }

        public static async Task PublishUserInformationChangedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            UserPersonalInfo information
        )
        {
            await publisher.PublishAsync(
                new UserInformationChangedIntegrationEvent(
                    userId,
                    information.FirstName!,
                    information.LastName!,
                    information.Gender! != Gender.Other ? information.Gender!.Name.ToLowerInvariant() : null,
                    information.BirthDate ?? throw new NullReferenceException(nameof(PublishUserInformationChangedIntegrationEventAsync))));
        }

        public static async Task PublishUserPhoneChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, string phoneNumber)
        {
            await publisher.PublishAsync(new UserPhoneChangedIntegrationEvent(userId, phoneNumber));
        }
    }
}
