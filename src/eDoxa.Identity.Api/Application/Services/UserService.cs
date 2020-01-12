// Filename: UserService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Services
{
    public sealed class UserService : UserManager<User>, IUserService
    {
        private readonly IServiceBusPublisher _publisher;

        public UserService(
            UserRepository repository,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserService> logger,
            IServiceBusPublisher publisher
        ) : base(
            repository,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger)
        {
            _publisher = publisher;
            ErrorDescriber = errors;
            Repository = repository;
        }

        public UserRepository Repository { get; }

        public async Task<IdentityResult> UpdateEmailAsync(User user, string email)
        {
            var result = await this.SetEmailAsync(user, email);

            if (result.Succeeded)
            {
                await _publisher.PublishUserEmailChangedIntegrationEventAsync(user);
            }

            return result;
        }

        public async Task<IdentityResult> UpdatePhoneNumberAsync(User user, string phoneNumber)
        {
            var result = await this.SetPhoneNumberAsync(user, phoneNumber);

            if (result.Succeeded)
            {
                await _publisher.PublishUserPhoneChangedIntegrationEventAsync(user);
            }

            return result;
        }

        public async Task<UserProfile?> GetProfileAsync(User user)
        {
            return await Repository.GetProfileAsync(user, CancellationToken);
        }

        public async Task<IDomainValidationResult> CreateProfileAsync(
            User user,
            string firstName,
            string lastName,
            Gender gender,
            int dobYear,
            int dobMonth,
            int dobDay
        )
        {
            var result = new DomainValidationResult();

            if (user.Profile != null)
            {
                result.AddFailedPreconditionError("The user's profile has already been created.");
            }

            if (result.IsValid)
            {
                var profile = new UserProfile(
                    firstName,
                    lastName,
                    gender,
                    new UserDob(dobYear, dobMonth, dobDay));

                user.Create(profile);

                //await this.UpdateSecurityStampAsync(user);

                await this.UpdateUserAsync(user);

                await _publisher.PublishUserProfileChangedIntegrationEventAsync(UserId.FromGuid(user.Id), profile);

                result.AddEntityToMetadata(profile);
            }

            return result;
        }

        public async Task<IDomainValidationResult> UpdateProfileAsync(User user, string firstName)
        {
            var result = new DomainValidationResult();

            if (user.Profile == null)
            {
                result.AddFailedPreconditionError("The user's personal informations does not exist.");
            }

            if (result.IsValid)
            {
                user.Update(firstName);

                //await this.UpdateSecurityStampAsync(user);

                await this.UpdateUserAsync(user);

                await _publisher.PublishUserProfileChangedIntegrationEventAsync(UserId.FromGuid(user.Id), user.Profile!);

                result.AddEntityToMetadata(user.Profile!);
            }

            return result;
        }

        public async Task<UserDob?> GetDobAsync(User user)
        {
            return await Repository.GetDobAsync(user, CancellationToken);
        }

        public async Task<string?> GetFirstNameAsync(User user)
        {
            return await Repository.GetFirstNameAsync(user, CancellationToken);
        }

        public async Task<string?> GetLastNameAsync(User user)
        {
            return await Repository.GetLastNameAsync(user, CancellationToken);
        }

        public async Task<Gender?> GetGenderAsync(User user)
        {
            return await Repository.GetGenderAsync(user, CancellationToken);
        }

        public async Task<Country> GetCountryAsync(User user)
        {
            return await Repository.GetCountryAsync(user, CancellationToken);
        }
    }
}
