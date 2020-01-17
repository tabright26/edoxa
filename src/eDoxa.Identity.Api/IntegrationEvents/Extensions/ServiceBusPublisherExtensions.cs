// Filename: ServiceBusPublisherExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.Dtos;
using eDoxa.Grpc.Protos.Identity.Enums;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusPublisherExtensions
    {
        public static async Task PublishUserPasswordResetTokenGeneratedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, string code)
        {
            var integrationEvent = new UserPasswordResetTokenGeneratedIntegrationEvent
            {
                UserId = userId.ToString(),
                Code = code
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserEmailConfirmationTokenGeneratedIntegrationEventAsync(
            this IServiceBusPublisher publisher,
            UserId userId,
            string code
        )
        {
            var integrationEvent = new UserEmailConfirmationTokenGeneratedIntegrationEvent
            {
                UserId = userId.ToString(),
                Code = code
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserCreatedIntegrationEventAsync(this IServiceBusPublisher publisher, User user)
        {
            var integrationEvent = new UserCreatedIntegrationEvent
            {
                UserId = user.Id.ToString(),
                Email = new EmailDto
                {
                    Address = user.Email,
                    Verified = user.EmailConfirmed
                },
                CountryIsoCode = user.Country.ToEnum<EnumCountryIsoCode>()
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserAddressChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, Address address)
        {
            var integrationEvent = new UserAddressChangedIntegrationEvent
            {
                UserId = userId,
                Address = new AddressDto
                {
                    Id = address.Id,
                    Type = address.Type.ToEnumOrDefault<EnumAddressType>(),
                    CountryIsoCode = address.Country.ToEnum<EnumCountryIsoCode>(),
                    Line1 = address.Line1,
                    Line2 = address.Line2,
                    State = address.State,
                    City = address.City,
                    PostalCode = address.PostalCode
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserEmailChangedIntegrationEventAsync(this IServiceBusPublisher publisher, User user)
        {
            var integrationEvent = new UserEmailChangedIntegrationEvent
            {
                UserId = user.Id.ToString(),
                Email = new EmailDto
                {
                    Address = user.Email,
                    Verified = user.EmailConfirmed
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserProfileChangedIntegrationEventAsync(this IServiceBusPublisher publisher, UserId userId, UserProfile profile)
        {
            var integrationEvent = new UserProfileChangedIntegrationEvent
            {
                UserId = userId,
                Profile = new ProfileDto
                {
                    FirstName = profile.FirstName,
                    LastName = profile.LastName,
                    Gender = profile.Gender.ToEnum<EnumGender>(),
                    Dob = new DobDto
                    {
                        Day = profile.Dob.Day,
                        Month = profile.Dob.Month,
                        Year = profile.Dob.Year
                    }
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }

        public static async Task PublishUserPhoneChangedIntegrationEventAsync(this IServiceBusPublisher publisher, User user)
        {
            var integrationEvent = new UserPhoneChangedIntegrationEvent
            {
                UserId = user.Id.ToString(),
                Phone = new PhoneDto
                {
                    Number = user.PhoneNumber,
                    Verified = user.PhoneNumberConfirmed
                }
            };

            await publisher.PublishAsync(integrationEvent);
        }
    }
}
