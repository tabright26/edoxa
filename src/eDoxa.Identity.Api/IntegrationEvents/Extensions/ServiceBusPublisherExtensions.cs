// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Grpc.Protos.Notifications.IntegrationEvents;
using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Seedwork.Domain.Misc;
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
                new EmailSentIntegrationEvent
                {
                    UserId = userId,
                    Email = email,
                    Subject = subject,
                    HtmlMessage = htmlMessage
                });
        }

        public static async Task PublishUserCreatedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string email,
            Country country
        )
        {
            await publisher.PublishAsync(
                new UserCreatedIntegrationEvent
                {
                    UserId = userId,
                    Email = email,
                    Country = (Grpc.Protos.Identity.Enums.Country) country.Value
                });
        }

        public static async Task PublishUserAddressChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, Address address)
        {
            await publisher.PublishAsync(
                new UserAddressChangedIntegrationEvent
                {
                    UserId = userId,
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    State = address.State,
                    City = address.City,
                    PostalCode = address.PostalCode
                });
        }

        public static async Task PublishUserEmailChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, string email)
        {
            await publisher.PublishAsync(
                new UserEmailChangedIntegrationEvent
                {
                    UserId = userId,
                    Email = email
                });
        }

        public static async Task PublishUserInformationChangedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string firstName,
            string lastName,
            Gender gender,
            Dob dob
        )
        {
            await publisher.PublishAsync(
                new UserInformationChangedIntegrationEvent
                {
                    UserId = userId,
                    FirstName = firstName,
                    LastName = lastName,
                    Gender = (Grpc.Protos.Identity.Enums.Gender) gender.Value,
                    Dob = new UserInformationChangedIntegrationEvent.Types.Dob
                    {
                        Day = dob.Day,
                        Month = dob.Month,
                        Year = dob.Year
                    }
                });
        }

        public static async Task PublishUserPhoneChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, string phoneNumber)
        {
            await publisher.PublishAsync(
                new UserPhoneChangedIntegrationEvent
                {
                    UserId = userId,
                    PhoneNumber = phoneNumber
                });
        }
    }
}
