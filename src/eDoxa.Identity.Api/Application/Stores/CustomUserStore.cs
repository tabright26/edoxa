// Filename: CustomUserStore.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Identity.Api.Application.Stores
{
    public class CustomUserStore : Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore<User, Role, IdentityDbContext, Guid, UserClaim,
        UserRole, UserLogin, UserToken, RoleClaim>
    {
        public CustomUserStore(IdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        private DbSet<UserGameProvider> UserGameProviders => Context.Set<UserGameProvider>();

        public virtual Task AddGameProviderAsync(User user, UserGameProviderInfo gameProvider, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (gameProvider == null)
            {
                throw new ArgumentNullException(nameof(gameProvider));
            }

            UserGameProviders.Add(this.CreateUserGameProvider(user, gameProvider));

            return Task.FromResult(false);
        }

        public async Task RemoveGameProviderAsync(User user, int gameValue, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var gameProvider = await this.FindUserGameProviderAsync(user.Id, gameValue, cancellationToken);

            if (gameProvider == null)
            {
                return;
            }

            UserGameProviders.Remove(gameProvider);
        }

        [ItemCanBeNull]
        public Task<UserGameProvider> FindUserGameProviderAsync(Guid userId, int gameValue, CancellationToken cancellationToken = default)
        {
            return UserGameProviders.SingleOrDefaultAsync(
                gameProvider => gameProvider.UserId == userId && gameProvider.Game == gameValue,
                cancellationToken
            );
        }

        [ItemCanBeNull]
        protected Task<UserGameProvider> FindUserGameProviderAsync(
            int gameValue,
            string playerId,
            CancellationToken cancellationToken = default
        )
        {
            return UserGameProviders.SingleOrDefaultAsync(
                gameProvider => gameProvider.Game == gameValue && gameProvider.PlayerId == playerId,
                cancellationToken
            );
        }

        public async Task<IList<UserGameProviderInfo>> GetGameProvidersAsync(User user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await UserGameProviders.Where(gameProvider => gameProvider.UserId.Equals(user.Id))
                .Select(gameProvider => new UserGameProviderInfo(Game.FromValue(gameProvider.Game), gameProvider.PlayerId))
                .ToListAsync(cancellationToken);
        }

        [ItemCanBeNull]
        public async Task<User> FindByGameProviderAsync(int gameValue, string playerId, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            var gameProvider = await this.FindUserGameProviderAsync(gameValue, playerId, cancellationToken);

            if (gameProvider != null)
            {
                return await this.FindUserAsync(gameProvider.UserId, cancellationToken);
            }

            return default;
        }

        protected UserGameProvider CreateUserGameProvider(User user, UserGameProviderInfo gameProvider)
        {
            return new UserGameProvider
            {
                UserId = user.Id,
                Game = gameProvider.Game.Value,
                PlayerId = gameProvider.PlayerId
            };
        }
    }
}
