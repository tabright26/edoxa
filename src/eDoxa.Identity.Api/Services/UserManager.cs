// Filename: UserManager.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Services
{
    public sealed class UserManager : UserManager<User>, IUserManager
    {
        private readonly IServiceBusPublisher _publisher;

        public UserManager(
            UserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            CustomIdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager> logger,
            IServiceBusPublisher publisher
        ) : base(
            store,
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
            Store = store;
        }

        private new CustomIdentityErrorDescriber ErrorDescriber { get; }

        public new UserStore Store { get; }

        public override async Task<IdentityResult> SetEmailAsync(User user, string email)
        {
            var result = await base.SetEmailAsync(user, email);

            if (result.Succeeded)
            {
                await _publisher.PublishUserEmailChangedIntegrationEventAsync(UserId.FromGuid(user.Id), email);
            }

            return result;
        }

        public override async Task<IdentityResult> SetPhoneNumberAsync(User user, string phoneNumber)
        {
            var result = await base.SetPhoneNumberAsync(user, phoneNumber);

            if (result.Succeeded)
            {
                await _publisher.PublishUserPhoneChangedIntegrationEventAsync(UserId.FromGuid(user.Id), phoneNumber);
            }

            return result;
        }

        public async Task<UserProfile?> GetInformationsAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetInformationsAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> CreateInformationsAsync(
            User user,
            string firstName,
            string lastName,
            Gender gender,
            Dob dob
        )
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await Store.SetInformationsAsync(
                user,
                new UserProfile(
                    firstName,
                    lastName,
                    gender,
                    dob),
                CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            var result = await this.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                await _publisher.PublishUserInformationChangedIntegrationEventAsync(
                    UserId.FromGuid(user.Id),
                    firstName,
                    lastName,
                    gender,
                    dob);
            }

            return result;
        }

        public async Task<IdentityResult> UpdateInformationsAsync(User user, string firstName)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var lastName = user.Profile!.LastName!;
            var gender = user.Profile!.Gender!;
            var dob = user.Profile!.Dob!;

            await Store.SetInformationsAsync(
                user,
                new UserProfile(
                    firstName,
                    lastName,
                    gender,
                    dob),
                CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            var result = await this.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                await _publisher.PublishUserInformationChangedIntegrationEventAsync(
                    UserId.FromGuid(user.Id),
                    firstName,
                    lastName,
                    gender,
                    dob);
            }

            return result;
        }

        public async Task<Dob?> GetDobAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetDobAsync(user, CancellationToken);
        }

        public async Task<string?> GetFirstNameAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetFirstNameAsync(user, CancellationToken);
        }

        public async Task<string?> GetLastNameAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetLastNameAsync(user, CancellationToken);
        }

        public async Task<Gender?> GetGenderAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetGenderAsync(user, CancellationToken);
        }

        public async Task<Country> GetCountryAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetCountryAsync(user, CancellationToken);
        }
    }
}
