﻿// Filename: UserStore.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Domain.AggregateModels;
using eDoxa.Identity.Domain.AggregateModels.AddressAggregate;
using eDoxa.Identity.Domain.AggregateModels.DoxatagAggregate;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class UserStore : Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore<User, Role, IdentityDbContext, Guid, UserClaim, UserRole,
        UserLogin, UserToken, RoleClaim>
    {
        public UserStore(IdentityDbContext context, CustomIdentityErrorDescriber describer) : base(context, describer)
        {
        }

        private DbSet<UserAddress> AddressBook => Context.Set<UserAddress>();

        public DbSet<UserDoxatag> DoxatagHistory => Context.Set<UserDoxatag>();

        public Task<UserInformations?> GetInformationsAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Informations);
        }

        public async Task<ICollection<UserDoxatag>> GetDoxatagHistoryAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await DoxatagHistory.Where(doxatag => doxatag.UserId == user.Id).OrderBy(doxatag => doxatag.Timestamp).ToListAsync(cancellationToken);
        }

        public async Task<UserDoxatag?> GetDoxatagAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var history = await this.GetDoxatagHistoryAsync(user, cancellationToken);

            return history.FirstOrDefault();
        }

        public Task SetDoxatagAsync(User user, UserDoxatag userDoxatag, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            DoxatagHistory.Add(userDoxatag);

            return Task.CompletedTask;
        }

        public async Task<IList<int>> GetCodesForDoxatagAsync(string doxatagName, CancellationToken cancellationToken = default)
        {
            return await DoxatagHistory.AsExpandable()
                .Where(doxatag => doxatag.Name.Equals(doxatagName, StringComparison.OrdinalIgnoreCase))
                .Select(doxatag => doxatag.Code)
                .ToListAsync(cancellationToken);
        }

        public Task AddAddressAsync(
            User user,
            Country country,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(line1))
            {
                throw new ArgumentNullException(nameof(line1));
            }

            if (string.IsNullOrWhiteSpace(city))
            {
                throw new ArgumentNullException(nameof(city));
            }

            if (string.IsNullOrWhiteSpace(postalCode))
            {
                throw new ArgumentNullException(nameof(postalCode));
            }

            AddressBook.Add(
                new UserAddress
                {
                    Id = Guid.NewGuid(),
                    Type = UserAddressType.Principal,
                    Country = country,
                    Line1 = line1,
                    Line2 = line2,
                    City = city,
                    State = state,
                    PostalCode = postalCode,
                    UserId = user.Id
                });

            return Task.FromResult(false);
        }

        public async Task RemoveAddressAsync(User user, Guid addressId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var address = await this.FindUserAddressAsync(user.Id, addressId, cancellationToken);

            if (address == null)
            {
                return;
            }

            AddressBook.Remove(address);
        }

        public async Task<UserAddress?> FindUserAddressAsync(Guid userId, Guid addressId, CancellationToken cancellationToken = default)
        {
            return await AddressBook.SingleOrDefaultAsync(address => address.UserId == userId && address.Id == addressId, cancellationToken);
        }

        public async Task<ICollection<UserAddress>> GetAddressBookAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await AddressBook.Where(address => address.UserId == user.Id).ToListAsync(cancellationToken);
        }

        public Task<string?> GetFirstNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Informations?.FirstName);
        }

        public Task<string?> GetLastNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Informations?.LastName);
        }

        public Task<Gender?> GetGenderAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Informations?.Gender);
        }

        public Task<Dob?> GetDobAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Informations?.Dob);
        }

        public Task SetInformationsAsync(User user, UserInformations userInformations, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Informations = userInformations;

            return Task.CompletedTask;
        }

        public Task<Country> GetCountryAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Country);
        }
    }
}
