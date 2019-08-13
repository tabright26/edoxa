// Filename: CustomUserManager.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Models;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class UserManager : UserManager<User>, IUserManager
    {
        private static readonly Random Random = new Random();

        public UserManager(
            CustomUserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            CustomIdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager> logger
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

        public async Task<IdentityResult> AddGameAsync(User user, string gameName, string playerId)
        {
            this.ThrowIfDisposed();

            if (string.IsNullOrWhiteSpace(gameName))
            {
                throw new ArgumentNullException(nameof(gameName));
            }

            if (string.IsNullOrWhiteSpace(playerId))
            {
                throw new ArgumentNullException(nameof(playerId));
            }

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            if (await this.HasGameAlreadyLinkedAsync(user, Game.FromName(gameName)!))
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(4, $"{nameof(this.AddGameAsync)} for user {userId} failed because it has already been linked to {gameName} game provider.");

                return IdentityResult.Failed(ErrorDescriber.GameAlreadyLinked());
            }

            if (await this.FindByGameAsync(Game.FromName(gameName)!, playerId) != null)
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(4, $"{nameof(this.AddGameAsync)} for user {userId} failed because it was already associated with another user.");

                return IdentityResult.Failed(ErrorDescriber.GameAlreadyAssociated());
            }

            await Store.AddGameAsync(user, gameName, playerId, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        public async Task<IdentityResult> RemoveGameAsync(User user, Game game)
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

            if (!await this.HasGameAlreadyLinkedAsync(user, game))
            {
                var userId = await this.GetUserIdAsync(user);

                Logger.LogWarning(4, $"{nameof(this.RemoveGameAsync)} for user {userId} failed because the {game} game provider is unlinked.");

                return IdentityResult.Failed(ErrorDescriber.GameNotAssociated());
            }

            await Store.RemoveGameAsync(user, game.Value, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        public Task<User?> FindByGameAsync(Game game, string playerId)
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

            return Store.FindByGameAsync(game.Value, playerId, CancellationToken);
        }

        public async Task<IList<UserGame>> GetGamesAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetGamesAsync(user, CancellationToken);
        }

        public async Task<PersonalInfo?> GetPersonalInfoAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetPersonalInfoAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> SetPersonalInfoAsync(User user, string? firstName, string? lastName, Gender? gender, DateTime? birthDate)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var profile = new PersonalInfo
            {
                FirstName = firstName,
                LastName = lastName,
                Gender = gender,
                BirthDate = birthDate
            };

            await Store.SetPersonalInfoAsync(user, profile, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        public async Task<Address?> GetAddressAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetAddressAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> SetAddressAsync(User user, string street, string city, string postalCode, string country)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var address = new Address
            {
                Street = street,
                City = city,
                PostalCode = postalCode,
                Country = country
            };

            await Store.SetAddressAsync(user, address, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        public async Task<DoxaTag?> GetDoxaTagAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetDoxatagAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> SetDoxaTagAsync(User user, string doxaTagName)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var doxaTag = new DoxaTag
            {
                Name = doxaTagName,
                Code = await this.EnsureCodeUniqueness(doxaTagName)
            };

            await Store.SetDoxatagAsync(user, doxaTag, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        private async Task<int> EnsureCodeUniqueness(string doxaTagName)
        {
            var codes = await Store.GetCodesForDoxaTagAsync(doxaTagName);

            return codes.Any() ? EnsureCodeUniqueness() : GenerateCode();

            int EnsureCodeUniqueness()
            {
                while (true)
                {
                    var code = GenerateCode();

                    if (codes.Contains(code))
                    {
                        continue;
                    }

                    return code;
                }
            }

            static int GenerateCode()
            {
                return Random.Next(100, 10000);
            }
        }

        public async Task<string?> GetBirthDateAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetBirthDateAsync(user, CancellationToken);
        }

        public async Task<string?> GetFirstNameAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetFirstNameAsync(user, CancellationToken);
        }

        public async Task<string?> GetLastNameAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetLastNameAsync(user, CancellationToken);
        }

        public async Task<Gender?> GetGenderAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetGenderAsync(user, CancellationToken);
        }

        private async Task<bool> HasGameAlreadyLinkedAsync(User user, Game game)
        {
            var games = await this.GetGamesAsync(user);

            return games.SingleOrDefault(userGame => Game.FromValue(userGame.Value)!.Name == game.Name) != null;
        }
    }
}
