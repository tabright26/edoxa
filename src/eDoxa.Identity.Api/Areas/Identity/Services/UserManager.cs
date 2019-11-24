// Filename: UserManager.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using LinqKit;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class UserManager : UserManager<User>, IUserManager
    {
        private static readonly Random Random = new Random();
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

        public async Task<IEnumerable<UserDoxatag>> FetchDoxatagsAsync()
        {
            this.ThrowIfDisposed();

            var doxatagHistory = await Store.DoxatagHistory.AsExpandable().ToListAsync();

            return doxatagHistory.GroupBy(doxatag => doxatag.UserId)
                .Select(history => history.OrderBy(doxatag => doxatag.Timestamp).First())
                .ToList();
        }

        public async Task<UserAddress?> FindUserAddressAsync(User user, Guid addressId)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.FindUserAddressAsync(user.Id, addressId);
        }

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

        public async Task<UserInformations?> GetInformationsAsync(User user)
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
                new UserInformations(
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

            var lastName = user.Informations!.LastName!;
            var gender = user.Informations!.Gender!;
            var dob = user.Informations!.Dob!;

            await Store.SetInformationsAsync(
                user,
                new UserInformations(
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

        public async Task<UserDoxatag?> GetDoxatagAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetDoxatagAsync(user, CancellationToken);
        }

        public async Task<ICollection<UserDoxatag>> GetDoxatagHistoryAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetDoxatagHistoryAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> SetDoxatagAsync(User user, string doxatagName)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var doxatag = new UserDoxatag
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Name = doxatagName,
                Code = await this.EnsureCodeUniqueness(doxatagName),
                Timestamp = DateTime.UtcNow
            };

            await Store.SetDoxatagAsync(user, doxatag, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
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

        public async Task<ICollection<UserAddress>> GetAddressBookAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetAddressBookAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> RemoveAddressAsync(User user, Guid addressId)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await Store.RemoveAddressAsync(user, addressId, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        public async Task<IdentityResult> AddAddressAsync(
            User user,
            Country country,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await Store.AddAddressAsync(
                user,
                country,
                line1,
                line2,
                city,
                state,
                postalCode,
                CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            var result = await this.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                await _publisher.PublishUserAddressChangedIntegrationEventAsync(
                    UserId.FromGuid(user.Id),
                    new UserAddress
                    {
                        City = city,
                        Country = country,
                        Line1 = line1,
                        Line2 = line2,
                        PostalCode = postalCode,
                        State = state
                    });
            }

            return result;
        }

        public async Task<IdentityResult> UpdateAddressAsync(
            User user,
            Guid addressId,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var address = await Store.FindUserAddressAsync(user.Id, addressId, CancellationToken);

            if (address == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "UserAddressNotFound",
                        Description = "User's address not found."
                    });
            }

            address.Line1 = line1;
            address.Line2 = line2;
            address.City = city;
            address.State = state;
            address.PostalCode = postalCode;

            await this.UpdateSecurityStampAsync(user);

            var result = await this.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                await _publisher.PublishUserAddressChangedIntegrationEventAsync(UserId.FromGuid(user.Id), address);
            }

            return result;
        }

        private async Task<int> EnsureCodeUniqueness(string doxatagName)
        {
            var codes = await Store.GetCodesForDoxatagAsync(doxatagName);

            return codes.Any() ? EnsureCodeUniqueness() : GenerateCode();

            int EnsureCodeUniqueness()
            {
                while (true)
                {
                    var code = GenerateCode();

                    if (codes.Contains(code))
                    {
                        continue;
                    }

                    return code;
                }
            }

            static int GenerateCode()
            {
                return Random.Next(100, 10000);
            }
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
