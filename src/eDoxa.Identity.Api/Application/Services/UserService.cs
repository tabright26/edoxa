// Filename: UserService.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Security.Claims;
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
            IOptionsSnapshot<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            IUserValidator<User> userValidator,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserService> logger,
            IServiceBusPublisher publisher
        ) : base(
            repository,
            optionsAccessor,
            passwordHasher,
            new List<IUserValidator<User>>
            {
                userValidator
            },
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

        public override Task<User> FindByNameAsync(string userName)
        {
            throw new NotImplementedException();
        }

        public override Task<IdentityResult> SetUserNameAsync(User user, string userName)
        {
            throw new NotImplementedException();
        }

        public override string GetUserName(ClaimsPrincipal principal)
        {
            throw new NotImplementedException();
        }

        public override Task<string> GetUserNameAsync(User user)
        {
            throw new NotImplementedException();
        }

        public override Task UpdateNormalizedUserNameAsync(User user)
        {
            throw new NotImplementedException();
        }

        public override async Task<IdentityResult> CreateAsync(User user)
        {
            this.ThrowIfDisposed();

            await this.UpdateSecurityStampInternal(user);

            var identityResult = await this.ValidateUserAsync(user);

            if (!identityResult.Succeeded)
            {
                return identityResult;
            }

            if (Options.Lockout.AllowedForNewUsers && SupportsUserLockout)
            {
                await Repository.SetLockoutEnabledAsync(user, true, CancellationToken);
            }

            await this.UpdateNormalizedEmailAsync(user);

            return await Repository.CreateAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> UpdateEmailAsync(User user, string email)
        {
            return await this.SetEmailAsync(user, email);
        }

        public async Task<IdentityResult> UpdatePhoneNumberAsync(User user, string phoneNumber)
        {
            return await this.SetPhoneNumberAsync(user, phoneNumber);
        }

        public async Task<UserProfile?> GetProfileAsync(User user)
        {
            return await Repository.GetProfileAsync(user, CancellationToken);
        }

        public async Task<IDomainValidationResult> CreateProfileAsync(
            User user,
            string firstName,
            string lastName,
            Gender gender
        )
        {
            var result = new DomainValidationResult();

            if (user.Profile != null)
            {
                result.AddFailedPreconditionError("The user's profile has already been created.");
            }

            if (result.IsValid)
            {
                var profile = new UserProfile(firstName, lastName, gender);

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

                await this.UpdateSecurityStampAsync(user);

                await this.UpdateUserAsync(user);

                await _publisher.PublishUserProfileChangedIntegrationEventAsync(UserId.FromGuid(user.Id), user.Profile!);

                result.AddEntityToMetadata(user.Profile!);
            }

            return result;
        }

        public async Task<UserDob> GetDobAsync(User user)
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

        protected override async Task<IdentityResult> UpdateUserAsync(User user)
        {
            var identityResult = await this.ValidateUserAsync(user);

            if (!identityResult.Succeeded)
            {
                return identityResult;
            }

            await this.UpdateNormalizedEmailAsync(user);

            return await Repository.UpdateAsync(user, CancellationToken);
        }

        public override async Task<IdentityResult> SetEmailAsync(User user, string email)
        {
            var result = await base.SetEmailAsync(user, email);

            if (result.Succeeded)
            {
                await _publisher.PublishUserEmailChangedIntegrationEventAsync(user);
            }

            return result;
        }

        public override async Task<IdentityResult> SetPhoneNumberAsync(User user, string phoneNumber)
        {
            var result = await base.SetPhoneNumberAsync(user, phoneNumber);

            if (result.Succeeded)
            {
                await _publisher.PublishUserPhoneChangedIntegrationEventAsync(user);
            }

            return result;
        }

        private async Task UpdateSecurityStampInternal(User user)
        {
            if (!SupportsUserSecurityStamp)
            {
                return;
            }

            await Repository.SetSecurityStampAsync(user, Guid.NewGuid().ToString("N"), CancellationToken);
        }
    }
}
