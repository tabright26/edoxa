// Filename: UserService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;

using eDoxa.Identity.Application.IntegrationEvents;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Domain.Repositories;
using eDoxa.ServiceBus;
using JetBrains.Annotations;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;

namespace eDoxa.Identity.Application.Services
{
    public class UserService : UserManager<User>, IUserService
    {
        private readonly IIntegrationEventService _integrationEventService;

        public UserService(
            IIntegrationEventService integrationEventService,
            IUserRepository userRepository,
            IOptions<IdentityOptions> optionsAccessor,
            IPasswordHasher<User> passwordHasher,
            IEnumerable<IUserValidator<User>> userValidators,
            IEnumerable<IPasswordValidator<User>> passwordValidators,
            ILookupNormalizer keyNormalizer,
            IdentityErrorDescriber errors,
            IServiceProvider services,
            ILogger<UserManager<User>> logger) : base(
            userRepository,
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
            _integrationEventService = integrationEventService;
        }

        public async Task ChangeStatusAsync(Guid userId, UserStatus status)
        {
            this.ThrowIfDisposed();

            var user = await this.FindUserAsync(userId);

            // TODO: Refactor.
            if (status == UserStatus.Unknown || status == UserStatus.Offline)
            {
                throw new InvalidEnumArgumentException(nameof(status), (int) status, typeof(UserStatus));
            }

            user.ChangeStatus(status);

            await this.UpdateAsync(user);
        }

        public async Task ChangeTagAsync(Guid userId, string username)
        {
            this.ThrowIfDisposed();

            var user = await this.FindUserAsync(userId);

            user.ChangeTag(username);

            await this.UpdateAsync(user);
        }

        public async Task<bool> UserExistsAsync(Guid userId)
        {
            this.ThrowIfDisposed();

            return await Users.AsNoTracking().AnyAsync(user => user.Id == userId);
        }

        public async Task<IdentityResult> ConnectAsync(User user)
        {
            this.ThrowIfDisposed();

            user.Connect();

            return await this.UpdateAsync(user);
        }

        [ItemNotNull]
        public override async Task<IdentityResult> CreateAsync([NotNull] User user, [NotNull] string password)
        {
            this.ThrowIfDisposed();

            var result = await base.CreateAsync(user, password);

            if (!result.Succeeded)
            {
                return result;
            }

            await _integrationEventService.PublishAsync(new UserCreatedIntegrationEvent(user.Id, user.Email));

            return result;
        }

        public async Task<IdentityResult> DisconnectAsync(User user)
        {
            this.ThrowIfDisposed();

            user.Disconnect();

            return await this.UpdateAsync(user);
        }

        public async Task<User> FindUserAsync(Guid userId)
        {
            this.ThrowIfDisposed();

            return await Users.SingleOrDefaultAsync(user => user.Id == userId);
        }

        public async Task<string> GetUserNameAsync(string email)
        {
            this.ThrowIfDisposed();

            var user = await this.FindByEmailAsync(email);

            return user.UserName;
        }

        public async Task<string> GetNameAsync(User user)
        {
            this.ThrowIfDisposed();

            return await Task.Run(() => user.Name.ToString());
        }

        public async Task<string> GetBirthDateAsync(User user)
        {
            this.ThrowIfDisposed();

            return await Task.Run(() => user.BirthDate.ToString());
        }

        public async Task<string> GetTagAsync(User user)
        {
            this.ThrowIfDisposed();

            return await Task.Run(() => user.Tag.ToString());
        }
    }
}