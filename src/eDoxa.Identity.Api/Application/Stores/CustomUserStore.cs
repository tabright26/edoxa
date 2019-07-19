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
    public class CustomUserStore : Microsoft.AspNetCore.Identity.EntityFrameworkCore.UserStore<UserModel, RoleModel, IdentityDbContext, Guid, UserClaimModel,
        UserRoleModel, UserLoginModel, UserTokenModel, RoleClaimModel>
    {
        public CustomUserStore(IdentityDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        private DbSet<UserGameProviderModel> UserGameProviders => Context.Set<UserGameProviderModel>();

        public virtual Task AddGameProviderAsync(UserModel user, UserGameProviderInfo gameProvider, CancellationToken cancellationToken = default)
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

        public async Task RemoveGameProviderAsync(UserModel user, int gameProviderValue, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var gameProviderModel = await this.FindUserGameProviderAsync(user.Id, gameProviderValue, cancellationToken);

            if (gameProviderModel == null)
            {
                return;
            }

            UserGameProviders.Remove(gameProviderModel);
        }

        [ItemCanBeNull]
        public Task<UserGameProviderModel> FindUserGameProviderAsync(Guid userId, int gameProviderValue, CancellationToken cancellationToken = default)
        {
            return UserGameProviders.SingleOrDefaultAsync(
                gameProvider => gameProvider.UserId == userId && gameProvider.GameProvider == gameProviderValue,
                cancellationToken
            );
        }

        [ItemCanBeNull]
        protected Task<UserGameProviderModel> FindUserGameProviderAsync(
            int gameProviderValue,
            string providerKey,
            CancellationToken cancellationToken = default
        )
        {
            return UserGameProviders.SingleOrDefaultAsync(
                gameProvider => gameProvider.GameProvider == gameProviderValue && gameProvider.ProviderKey == providerKey,
                cancellationToken
            );
        }

        public async Task<IList<UserGameProviderInfo>> GetGameProvidersAsync(UserModel user, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await UserGameProviders.Where(gameProvider => gameProvider.UserId.Equals(user.Id))
                .Select(gameProvider => new UserGameProviderInfo(GameProvider.FromValue(gameProvider.GameProvider), gameProvider.ProviderKey))
                .ToListAsync(cancellationToken);
        }

        [ItemCanBeNull]
        public async Task<UserModel> FindByGameProviderAsync(int gameProviderValue, string providerKey, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            var gameProvider = await this.FindUserGameProviderAsync(gameProviderValue, providerKey, cancellationToken);

            if (gameProvider != null)
            {
                return await this.FindUserAsync(gameProvider.UserId, cancellationToken);
            }

            return default;
        }

        protected UserGameProviderModel CreateUserGameProvider(UserModel user, UserGameProviderInfo gameProvider)
        {
            return new UserGameProviderModel
            {
                UserId = user.Id,
                GameProvider = gameProvider.GameProvider.Value,
                ProviderKey = gameProvider.ProviderKey
            };
        }
    }
}
