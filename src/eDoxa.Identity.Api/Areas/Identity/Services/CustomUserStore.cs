// Filename: CustomUserStore.cs
// Date Created: 2019-07-21
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

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class CustomUserStore : Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore<User, Role, IdentityDbContext, Guid, UserClaim, UserRole,
        UserLogin, UserToken, RoleClaim>
    {
        public CustomUserStore(IdentityDbContext context, CustomIdentityErrorDescriber describer) : base(context, describer)
        {
        }

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
                }
            );

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

            return await UserGames.Where(game => game.UserId.Equals(user.Id)).ToListAsync(cancellationToken);
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

        public Task<PersonalInfo?> GetPersonalInfoAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PersonalInfo);
        }

        public Task<DoxaTag?> GetDoxatagAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.DoxaTag);
        }

        public Task SetDoxatagAsync(User user, DoxaTag doxaTag, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.DoxaTag = doxaTag;

            return Task.CompletedTask;
        }

        public async Task<IList<int>> GetCodesForDoxaTagAsync(string doxaTagName, CancellationToken cancellationToken = default)
        {
            return await Users.Where(user => user.DoxaTag != null && user.DoxaTag.Name.Contains(doxaTagName, StringComparison.OrdinalIgnoreCase))
                .Select(user => user.DoxaTag!.Code)
                .ToListAsync(cancellationToken);
        }

        public Task<Address?> GetAddressAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Address);
        }

        public Task SetAddressAsync(User user, Address address, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.Address = address;

            return Task.CompletedTask;
        }

        public Task<string?> GetFirstNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PersonalInfo?.FirstName);
        }

        public Task<string?> GetLastNameAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PersonalInfo?.LastName);
        }

        public Task<Gender?> GetGenderAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PersonalInfo?.Gender);
        }

        public Task<string?> GetBirthDateAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.PersonalInfo?.BirthDate?.ToString("yyyy-MM-dd"));
        }

        public Task SetPersonalInfoAsync(User user, PersonalInfo personalInfo, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            user.PersonalInfo = personalInfo;

            return Task.CompletedTask;
        }
    }
}
