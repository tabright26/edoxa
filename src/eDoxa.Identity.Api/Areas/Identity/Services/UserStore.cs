// Filename: UserStore.cs
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
using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Miscs;

using Microsoft.EntityFrameworkCore;

using UserClaim = eDoxa.Identity.Api.Infrastructure.Models.UserClaim;

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

        private DbSet<UserGame> UserGames => Context.Set<UserGame>();

        public Task AddGameAsync(
            User user,
            string gameName,
            string playerId,
            CancellationToken cancellationToken = default
        )
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (string.IsNullOrWhiteSpace(gameName))
            {
                throw new ArgumentNullException(nameof(gameName));
            }

            if (string.IsNullOrWhiteSpace(playerId))
            {
                throw new ArgumentNullException(nameof(playerId));
            }

            UserGames.Add(
                new UserGame
                {
                    Value = Game.FromName(gameName)!.Value,
                    PlayerId = playerId,
                    UserId = user.Id
                });

            return Task.FromResult(false);
        }

        public async Task RemoveGameAsync(User user, int gameValue, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var game = await this.FindUserGameAsync(user.Id, gameValue, cancellationToken);

            if (game == null)
            {
                return;
            }

            UserGames.Remove(game);
        }

        public async Task<UserGame?> FindUserGameAsync(Guid userId, int gameValue, CancellationToken cancellationToken = default)
        {
            return await UserGames.SingleOrDefaultAsync(game => game.UserId == userId && game.Value == gameValue, cancellationToken);
        }

        private async Task<UserGame?> FindUserGameAsync(int gameValue, string playerId, CancellationToken cancellationToken = default)
        {
            return await UserGames.SingleOrDefaultAsync(game => game.Value == gameValue && game.PlayerId == playerId, cancellationToken);
        }

        public async Task<IList<UserGame>> GetGamesAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await UserGames.Where(game => game.UserId == user.Id).ToListAsync(cancellationToken);
        }

        public async Task<User?> FindByGameAsync(int gameValue, string playerId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            var game = await this.FindUserGameAsync(gameValue, playerId, cancellationToken);

            if (game != null)
            {
                return await this.FindUserAsync(game.UserId, cancellationToken);
            }

            return default;
        }

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
            return await DoxatagHistory.Where(doxatag => doxatag.Name.Contains(doxatagName, StringComparison.OrdinalIgnoreCase))
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
