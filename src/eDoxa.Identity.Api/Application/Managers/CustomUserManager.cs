// Filename: CustomUserManager.cs
// Date Created: 2019-07-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Extensions;
using eDoxa.Identity.Api.Application.Stores;
using eDoxa.Identity.Api.Models;

using JetBrains.Annotations;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Application.Managers
{
    public sealed class CustomUserManager : UserManager<UserModel>
    {
        private static readonly Random Random = new Random();

        public CustomUserManager(
            CustomUserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<UserModel> passwordHasher,
            IEnumerable<IUserValidator<UserModel>> userValidators,
            IEnumerable<IPasswordValidator<UserModel>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
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
            Store = store;
        }

        private new CustomUserStore Store { get; }

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
        public override async Task<IdentityResult> SetUserNameAsync([NotNull] UserModel userModel, [NotNull] string userName)
        {
            var tags = await this.GetExistingTagsAsync(userName);

            var tag = tags.Any() ? GenerateUniqueTag(tags) : GenerateTag();

            return await base.SetUserNameAsync(userModel, BuildGamertag(userName, tag));
        }

        private async Task<IReadOnlyCollection<int>> GetExistingTagsAsync(string userName)
        {
            return await Users.Where(user => user.UserName.Contains(userName))
                .Select(user => user.UserName.Split('#', StringSplitOptions.None).Last())
                .Cast<int>()
                .ToListAsync();
        }

        public async Task<IdentityResult> AddGameProviderAsync(UserModel user, UserGameProviderInfo gameProvider)
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

            if (await this.HasGameProviderAlreadyLinkedAsync(user, gameProvider.GameProvider))
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(
                    4,
                    $"{nameof(this.AddGameProviderAsync)} for user {userId} failed because it has already been linked to {gameProvider.GameProvider} game provider."
                );

                return IdentityResult.Failed(ErrorDescriber.GameProviderAlreadyLinked());
            }

            if (await this.FindByGameProviderAsync(gameProvider.GameProvider, gameProvider.ProviderKey) != null)
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(4, $"{nameof(this.AddGameProviderAsync)} for user {userId} failed because it was already associated with another user.");

                return IdentityResult.Failed(ErrorDescriber.GameProviderAlreadyAssociated());
            }

            await Store.AddGameProviderAsync(user, gameProvider, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        public async Task<IdentityResult> RemoveGameProviderAsync(UserModel user, GameProvider gameProvider)
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

            if (!await this.HasGameProviderAlreadyLinkedAsync(user, gameProvider))
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(4, $"{nameof(this.RemoveGameProviderAsync)} for user {userId} failed because the {gameProvider} game provider is unlinked.");

                return IdentityResult.Failed(ErrorDescriber.GameProviderUnlinked());
            }

            await Store.RemoveGameProviderAsync(user, gameProvider.Value, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        [ItemCanBeNull]
        public Task<UserModel> FindByGameProviderAsync(GameProvider gameProvider, string providerKey)
        {
            this.ThrowIfDisposed();

            if (gameProvider == null)
            {
                throw new ArgumentNullException(nameof(gameProvider));
            }

            if (providerKey == null)
            {
                throw new ArgumentNullException(nameof(providerKey));
            }

            return Store.FindByGameProviderAsync(gameProvider.Value, providerKey, CancellationToken);
        }

        public async Task<IList<UserGameProviderInfo>> GetGameProvidersAsync(UserModel user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetGameProvidersAsync(user, CancellationToken);
        }

        public async Task<IList<UserGameProviderLinkInfo>> GetGameProviderLinksAsync(UserModel user)
        {
            var gameProviderInfos = await this.GetGameProvidersAsync(user);

            return GameProvider.GetEnumerations()
                .Select(
                    gameProvider =>
                    {
                        return new UserGameProviderLinkInfo
                        {
                            GameProvider = gameProvider,
                            IsLinked = gameProviderInfos.SingleOrDefault(provider => provider.GameProvider == gameProvider) != null
                        };
                    }
                )
                .OrderBy(gameProvider => gameProvider.GameProvider)
                .ToList();
        }

        private async Task<bool> HasGameProviderAlreadyLinkedAsync(UserModel user, GameProvider gameProvider)
        {
            var gameProviderLinks = await this.GetGameProviderLinksAsync(user);

            return gameProviderLinks.Single(gameProviderLink => gameProviderLink.GameProvider == gameProvider).IsLinked;
        }
    }
}
