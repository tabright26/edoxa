// Filename: CustomUserManager.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Describers;
using eDoxa.Identity.Api.Application.Stores;
using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Managers
{
    public sealed class CustomUserManager : UserManager<User>
    {
        private static readonly Random Random = new Random();

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

        private static int GenerateUniqueTag(IReadOnlyCollection<int> tags)
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

        private static string BuildGamertag(string userName, int tag)
        {
            return $"{userName}#{tag}";
        }

        [NotNull]
        [ItemNotNull]
        public override async Task<IdentityResult> SetUserNameAsync([NotNull] User user, [NotNull] string userName)
        {
            var tags = await this.GetExistingTagsAsync(userName);

            var tag = tags.Any() ? GenerateUniqueTag(tags) : GenerateTag();

            return await base.SetUserNameAsync(user, BuildGamertag(userName, tag));
        }

        private async Task<IReadOnlyCollection<int>> GetExistingTagsAsync(string userName)
        {
            return await Users.Where(user => user.UserName.Contains(userName))
                .Select(user => user.UserName.Split('#', StringSplitOptions.None).Last())
                .Cast<int>()
                .ToListAsync();
        }

        public async Task<IdentityResult> AddGameProviderAsync(User user, UserGameProviderInfo gameProvider)
        {
            this.ThrowIfDisposed();

            if (gameProvider == null)
            {
                throw new ArgumentNullException(nameof(gameProvider));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (await this.HasGameProviderAlreadyLinkedAsync(user, gameProvider.Game))
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(
                    4,
                    $"{nameof(this.AddGameProviderAsync)} for user {userId} failed because it has already been linked to {gameProvider.Game} game provider."
                );

                return IdentityResult.Failed(ErrorDescriber.GameProviderAlreadyLinked());
            }

            if (await this.FindByGameProviderAsync(gameProvider.Game, gameProvider.PlayerId) != null)
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(4, $"{nameof(this.AddGameProviderAsync)} for user {userId} failed because it was already associated with another user.");

                return IdentityResult.Failed(ErrorDescriber.GameProviderAlreadyAssociated());
            }

            await Store.AddGameProviderAsync(user, gameProvider, CancellationToken);

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

        public async Task<IList<UserGameProviderInfo>> GetGameProvidersAsync(User user)
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
            var gameProviderInfos = await this.GetGameProvidersAsync(user);

            return Game.GetEnumerations()
                .Select(
                    game =>
                    {
                        return new UserGameProviderLinkInfo
                        {
                            Game = game,
                            IsLinked = gameProviderInfos.SingleOrDefault(provider => provider.Game == game) != null
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
    }
}
