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

using eDoxa.Identity.Api.Application;
using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using IdentityDbContext = eDoxa.Identity.Api.Infrastructure.IdentityDbContext;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public class CustomUserStore : UserStore<User, Role, IdentityDbContext, Guid, UserClaim, UserRole, UserLogin, UserToken, RoleClaim>
    {
        private static readonly Random Random = new Random();

        public CustomUserStore(IdentityDbContext context, CustomIdentityErrorDescriber describer = null) : base(context, describer)
        {
        }

        private DbSet<UserGameProvider> UserGameProviders => Context.Set<UserGameProvider>();

        private static int EnsureUniqueTag(IReadOnlyCollection<int> tags)
        {
            while (true)
            {
                var tag = GenerateTag();

                if (tags.Contains(tag))
                {
                    continue;
                }

                return tag;
            }
        }

        private static int GenerateTag()
        {
            return Random.Next(100, 10000);
        }

        private static string FormatUserName(string normalizedName, int tag)
        {
            return $"{normalizedName}#{tag}";
        }

        private async Task<IReadOnlyCollection<int>> GetUserNameTagsAsync(string userName)
        {
            var users = await Users.Where(user => user.NormalizedUserName.Contains(userName, StringComparison.OrdinalIgnoreCase)).ToListAsync();

            return users.Select(user => user.NormalizedUserName.Split('#').Last()).Select(tag => Convert.ToInt32(tag)).ToList();
        }

        [NotNull]
        public override async Task SetUserNameAsync(
            [NotNull] User user,
            [NotNull] string userName,
            CancellationToken cancellationToken = new CancellationToken()
        )
        {
            var tags = await this.GetUserNameTagsAsync(userName);

            var tag = tags.Any() ? EnsureUniqueTag(tags) : GenerateTag();

            await base.SetUserNameAsync(user, FormatUserName(userName, tag), cancellationToken);
        }

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
            return UserGameProviders.SingleOrDefaultAsync(gameProvider => gameProvider.UserId == userId && gameProvider.Game == gameValue, cancellationToken);
        }

        [ItemCanBeNull]
        protected Task<UserGameProvider> FindUserGameProviderAsync(int gameValue, string playerId, CancellationToken cancellationToken = default)
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
                .Select(game => new UserGameProviderInfo(Game.FromValue(game.Game).Name, game.PlayerId))
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
                Game = Game.FromName(gameProvider.Name).Value,
                PlayerId = gameProvider.PlayerId
            };
        }

        [ItemCanBeNull]
        public Task<string> GetBirthDateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.BirthDate?.ToString("yyyy-MM-dd"));
        }

        [ItemCanBeNull]
        public Task<string> GetFirstNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.FirstName);
        }

        [ItemCanBeNull]
        public Task<string> GetLastNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.LastName);
        }

        [ItemCanBeNull]
        public Task<string> GetNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (user.FirstName == null)
            {
                return Task.FromResult((string) null);
            }

            if (user.LastName == null)
            {
                return Task.FromResult((string) null);
            }

            return Task.FromResult($"{user.FirstName} {user.LastName}");
        }
    }
}
