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

using eDoxa.Identity.Api.Infrastructure.Models;

using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

using IdentityDbContext = eDoxa.Identity.Api.Infrastructure.IdentityDbContext;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public class CustomUserStore : UserStore<User, Role, IdentityDbContext, Guid, UserClaim, UserRole,
        UserLogin, UserToken, RoleClaim>
    {
        private static readonly Random Random = new Random();

        public CustomUserStore(IdentityDbContext context, CustomIdentityErrorDescriber describer) : base(context, describer)
        {
        }

        private DbSet<UserGame> UserGames => Context.Set<UserGame>();

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

        
        public override async Task SetUserNameAsync(
             User user,
             string userName,
            CancellationToken cancellationToken = new CancellationToken()
        )
        {
            var tags = await this.GetUserNameTagsAsync(userName);

            var tag = tags.Any() ? EnsureUniqueTag(tags) : GenerateTag();

            await base.SetUserNameAsync(user, FormatUserName(userName, tag), cancellationToken);
        }

        public virtual Task AddGameAsync(User user, string gameName, string playerId, CancellationToken cancellationToken = default)
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

            UserGames.Add(new UserGame
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

        protected async Task<UserGame?> FindUserGameAsync(int gameValue, string playerId, CancellationToken cancellationToken = default)
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

        public Task<string?> GetBirthDateAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Profile.BirthDate?.ToString("yyyy-MM-dd"));
        }

        public Task<string?> GetFirstNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Profile.FirstName);
        }

        public Task<string?> GetLastNameAsync(User user, CancellationToken cancellationToken)
        {
            cancellationToken.ThrowIfCancellationRequested();

            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return Task.FromResult(user.Profile.LastName);
        }
    }
}
