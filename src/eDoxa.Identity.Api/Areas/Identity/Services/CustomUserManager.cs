// Filename: CustomUserManager.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application;
using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class CustomUserManager : UserManager<User>
    {
        public CustomUserManager(
            CustomUserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            CustomIdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<CustomUserManager> logger
        ) : base(
            store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger
        )
        {
            ErrorDescriber = errors;
            Store = store;
        }

        private new CustomUserStore Store { get; }

        private new CustomIdentityErrorDescriber ErrorDescriber { get; }

        public async Task<IdentityResult> AddGameProviderAsync(User user, UserGameProviderInfo gameInfo)
        {
            this.ThrowIfDisposed();

            if (gameInfo == null)
            {
                throw new ArgumentNullException(nameof(gameInfo));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (await this.HasGameProviderAlreadyLinkedAsync(user, Game.FromName(gameInfo.Name)))
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(
                    4,
                    $"{nameof(this.AddGameProviderAsync)} for user {userId} failed because it has already been linked to {gameInfo.Name} game provider."
                );

                return IdentityResult.Failed(ErrorDescriber.GameProviderAlreadyLinked());
            }

            if (await this.FindByGameProviderAsync(Game.FromName(gameInfo.Name), gameInfo.PlayerId) != null)
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(4, $"{nameof(this.AddGameProviderAsync)} for user {userId} failed because it was already associated with another user.");

                return IdentityResult.Failed(ErrorDescriber.GameProviderAlreadyAssociated());
            }

            await Store.AddGameProviderAsync(user, gameInfo, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        public async Task<IdentityResult> RemoveGameProviderAsync(User user, Game game)
        {
            this.ThrowIfDisposed();

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (!await this.HasGameProviderAlreadyLinkedAsync(user, game))
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(4, $"{nameof(this.RemoveGameProviderAsync)} for user {userId} failed because the {game} game provider is unlinked.");

                return IdentityResult.Failed(ErrorDescriber.GameProviderUnlinked());
            }

            await Store.RemoveGameProviderAsync(user, game.Value, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        [ItemCanBeNull]
        public Task<User> FindByGameProviderAsync(Game game, string playerId)
        {
            this.ThrowIfDisposed();

            if (game == null)
            {
                throw new ArgumentNullException(nameof(game));
            }

            if (playerId == null)
            {
                throw new ArgumentNullException(nameof(playerId));
            }

            return Store.FindByGameProviderAsync(game.Value, playerId, CancellationToken);
        }

        public async Task<IList<UserGameProviderInfo>> GetGamesAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetGameProvidersAsync(user, CancellationToken);
        }

        public async Task<IList<UserGameProviderLinkInfo>> GetGameProviderLinksAsync(User user)
        {
            var gameInfos = await this.GetGamesAsync(user);

            return Game.GetEnumerations()
                .Select(
                    game =>
                    {
                        return new UserGameProviderLinkInfo
                        {
                            Game = game,
                            IsLinked = gameInfos.SingleOrDefault(gameInfo => gameInfo.Name == game.Name) != null
                        };
                    }
                )
                .OrderBy(gameProviderLink => gameProviderLink.Game)
                .ToList();
        }

        private async Task<bool> HasGameProviderAlreadyLinkedAsync(User user, Game game)
        {
            var gameProviderLinks = await this.GetGameProviderLinksAsync(user);

            return gameProviderLinks.Single(gameProviderLink => gameProviderLink.Game == game).IsLinked;
        }

        public async Task<string> GetBirthDateAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetBirthDateAsync(user, CancellationToken);
        }

        public async Task<string> GetFirstNameAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetFirstNameAsync(user, CancellationToken);
        }

        public async Task<string> GetLastNameAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetLastNameAsync(user, CancellationToken);
        }

        public async Task<string> GetNameAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetNameAsync(user, CancellationToken);
        }
    }
}
