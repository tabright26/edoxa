// Filename: UserManager.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Identity.Api.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Api.Areas.Identity.Services
{
    public sealed class UserManager : UserManager<User>, IUserManager
    {
        private static readonly Random Random = new Random();
        private readonly IServiceBusPublisher _publisher;

        public UserManager(
            UserStore store,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            CustomIdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager> logger,
            IServiceBusPublisher publisher
        ) : base(
            store,
            optionsAccessor,
            passwordHasher,
            userValidators,
            passwordValidators,
            keyNormalizer,
            errors,
            services,
            logger)
        {
            _publisher = publisher;
            ErrorDescriber = errors;
            Store = store;
        }

        private new CustomIdentityErrorDescriber ErrorDescriber { get; }

        public new UserStore Store { get; }

        public async Task<IEnumerable<UserDoxaTag>> FetchDoxaTagsAsync()
        {
            this.ThrowIfDisposed();

            return await Store.DoxaTagHistory.GroupBy(doxaTag => doxaTag.UserId)
                .Select(doxaTagHistory => doxaTagHistory.OrderBy(doxaTag => doxaTag.Timestamp).First())
                .ToListAsync();
        }

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

            await Store.AddGameAsync(
                user,
                gameName,
                playerId,
                CancellationToken);

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

        public async Task<UserAddress?> FindUserAddressAsync(User user, Guid addressId)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.FindUserAddressAsync(user.Id, addressId);
        }

        public override async Task<IdentityResult> SetEmailAsync(User user, string email)
        {
            var result = await base.SetEmailAsync(user, email);

            if (result.Succeeded)
            {
                await _publisher.PublishUserEmailChangedIntegrationEventAsync(UserId.FromGuid(user.Id), email);
            }

            return result;
        }

        public override async Task<IdentityResult> SetPhoneNumberAsync(User user, string phoneNumber)
        {
            var result = await base.SetPhoneNumberAsync(user, phoneNumber);

            if (result.Succeeded)
            {
                await _publisher.PublishUserPhoneChangedIntegrationEventAsync(UserId.FromGuid(user.Id), phoneNumber);
            }

            return result;
        }

        public async Task<UserPersonalInfo?> GetPersonalInfoAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetPersonalInfoAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> CreatePersonalInfoAsync(
            User user,
            string firstName,
            string lastName,
            Gender gender,
            DateTime dob
        )
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await Store.SetPersonalInfoAsync(
                user,
                new UserPersonalInfo
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Gender = gender,
                    BirthDate = dob
                },
                CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            var result = await this.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                await _publisher.PublishUserInformationChangedIntegrationEventAsync(
                    UserId.FromGuid(user.Id),
                    firstName,
                    lastName,
                    gender,
                    dob);
            }

            return result;
        }

        public async Task<IdentityResult> UpdatePersonalInfoAsync(User user, string firstName)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var lastName = user.PersonalInfo!.LastName!;
            var gender = user.PersonalInfo!.Gender!;
            var dob = user.PersonalInfo!.BirthDate!.Value!;

            await Store.SetPersonalInfoAsync(
                user,
                new UserPersonalInfo
                {
                    FirstName = firstName,
                    LastName = lastName,
                    Gender = gender,
                    BirthDate = dob
                },
                CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            var result = await this.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                await _publisher.PublishUserInformationChangedIntegrationEventAsync(
                    UserId.FromGuid(user.Id),
                    firstName,
                    lastName,
                    gender,
                    dob);
            }

            return result;
        }

        public async Task<UserDoxaTag?> GetDoxaTagAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetDoxaTagAsync(user, CancellationToken);
        }

        public async Task<ICollection<UserDoxaTag>> GetDoxaTagHistoryAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetDoxaTagHistoryAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> SetDoxaTagAsync(User user, string doxaTagName)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var doxaTag = new UserDoxaTag
            {
                Id = Guid.NewGuid(),
                UserId = user.Id,
                Name = doxaTagName,
                Code = await this.EnsureCodeUniqueness(doxaTagName),
                Timestamp = DateTime.UtcNow
            };

            await Store.SetDoxatagAsync(user, doxaTag, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
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

        public async Task<ICollection<UserAddress>> GetAddressBookAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetAddressBookAsync(user, CancellationToken);
        }

        public async Task<IdentityResult> RemoveAddressAsync(User user, Guid addressId)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await Store.RemoveAddressAsync(user, addressId, CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            return await this.UpdateUserAsync(user);
        }

        public async Task<IdentityResult> AddAddressAsync(
            User user,
            string country,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            await Store.AddAddressAsync(
                user,
                country,
                line1,
                line2,
                city,
                state,
                postalCode,
                CancellationToken);

            await this.UpdateSecurityStampAsync(user);

            var result = await this.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                await _publisher.PublishUserAddressChangedIntegrationEventAsync(
                    UserId.FromGuid(user.Id),
                    new UserAddress
                    {
                        City = city,
                        Country = country,
                        Line1 = line1,
                        Line2 = line2,
                        PostalCode = postalCode,
                        State = state
                    });
            }

            return result;
        }

        public async Task<IdentityResult> UpdateAddressAsync(
            User user,
            Guid addressId,
            string line1,
            string? line2,
            string city,
            string? state,
            string? postalCode
        )
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            var address = await Store.FindUserAddressAsync(user.Id, addressId, CancellationToken);

            if (address == null)
            {
                return IdentityResult.Failed(
                    new IdentityError
                    {
                        Code = "UserAddressNotFound",
                        Description = "User's address not found."
                    });
            }

            address.Line1 = line1;
            address.Line2 = line2;
            address.City = city;
            address.State = state;
            address.PostalCode = postalCode;

            await this.UpdateSecurityStampAsync(user);

            var result = await this.UpdateUserAsync(user);

            if (result.Succeeded)
            {
                await _publisher.PublishUserAddressChangedIntegrationEventAsync(UserId.FromGuid(user.Id), address);
            }

            return result;
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

        private async Task<bool> HasGameAlreadyLinkedAsync(User user, Game game)
        {
            var games = await this.GetGamesAsync(user);

            return games.SingleOrDefault(userGame => Game.FromValue(userGame.Value)!.Name == game.Name) != null;
        }

        public async Task<string> GetCountryAsync(User user)
        {
            this.ThrowIfDisposed();

            if (user == null)
            {
                throw new ArgumentNullException(nameof(user));
            }

            return await Store.GetCountryAsync(user, CancellationToken);
        }
    }
}
