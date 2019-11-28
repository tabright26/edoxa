// Filename: UserManager.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Services
{
    public sealed class UserManager : UserManager<User>, IUserManager
    {
        private static readonly Random Random = new Random();
        private readonly IServiceBusPublisher _publisher;
        private readonly IAddressRepository _addressRepository;
        private readonly IDoxatagRepository _doxatagRepository;

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
            IServiceBusPublisher publisher,
            IAddressRepository addressRepository,
            IDoxatagRepository doxatagRepository
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
            _addressRepository = addressRepository;
            _doxatagRepository = doxatagRepository;
            ErrorDescriber = errors;
            Store = store;
        }

        private new CustomIdentityErrorDescriber ErrorDescriber { get; }

        public new UserStore Store { get; }

        public async Task<IEnumerable<Doxatag>> FetchDoxatagsAsync()
        {
            this.ThrowIfDisposed();

            return await _doxatagRepository.FetchDoxatagsAsync();
        }

        public async Task<Address?> FindUserAddressAsync(User user, AddressId addressId)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _addressRepository.FindAddressAsync(UserId.FromGuid(user.Id), addressId);
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

        public async Task<Doxatag?> GetDoxatagAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _doxatagRepository.FindDoxatagAsync(UserId.FromGuid(user.Id));
        }

        public async Task<IReadOnlyCollection<Doxatag>> GetDoxatagHistoryAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await _doxatagRepository.FetchDoxatagHistoryAsync(UserId.FromGuid(user.Id));
        }

        public async Task<IdentityResult> SetDoxatagAsync(User user, string doxatagName)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var doxatag = new Doxatag(
                UserId.FromGuid(user.Id),
                doxatagName,
                await this.EnsureCodeUniqueness(doxatagName),
                new UtcNowDateTimeProvider());

            _doxatagRepository.Create(doxatag);

            await _doxatagRepository.UnitOfWork.CommitAsync();

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

        public async Task<IReadOnlyCollection<Address>> GetAddressBookAsync(User user)
        {
            return await _addressRepository.FetchAddressBookAsync(UserId.FromGuid(user.Id));
        }

        public async Task<IdentityResult> RemoveAddressAsync(User user, AddressId addressId)
        {
            this.ThrowIfDisposed();

            var address = await this.FindUserAddressAsync(user, addressId);

            if (address == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Description = "Address not found."
                    });
            }

            _addressRepository.Delete(address);

            await _addressRepository.UnitOfWork.CommitAsync();

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

            var address = new Address(
                UserId.FromGuid(user.Id),
                country,
                line1,
                line2,
                city,
                state,
                postalCode);

            user.AddressBook.Add(address);

            _addressRepository.Create(address);

            await _addressRepository.UnitOfWork.CommitAsync();

            await this.UpdateSecurityStampAsync(user);

            var result = await this.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                await _publisher.PublishUserAddressChangedIntegrationEventAsync(UserId.FromGuid(user.Id), address);
            }

            return result;
        }

        public async Task<IdentityResult> UpdateAddressAsync(
            User user,
            AddressId addressId,
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

            var address = await _addressRepository.FindAddressAsync(UserId.FromGuid(user.Id), addressId);

            if (address == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "UserAddressNotFound",
                        Description = "User's address not found."
                    });
            }

            address.Update(line1, line2, city, state, postalCode);
            
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
            var codes = await _doxatagRepository.FetchDoxatagCodesByNameAsync(doxatagName);

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
