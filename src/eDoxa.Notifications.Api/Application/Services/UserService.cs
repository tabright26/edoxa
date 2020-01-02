// Filename: UserService.cs
// Date Created: 2019-12-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Api.Application.Services
{
    public sealed class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;
        private readonly IEmailService _emailService;
        private readonly ILogger _logger;

        public UserService(IUserRepository userRepository, IEmailService emailService, ILogger<UserService> logger)
        {
            _userRepository = userRepository;
            _emailService = emailService;
            _logger = logger;
        }

        public async Task<bool> UserExistsAsync(UserId userId)
        {
            return await _userRepository.UserExistsAsync(userId);
        }

        public async Task<User> FindUserAsync(UserId userId)
        {
            return await _userRepository.FindUserAsync(userId);
        }

        public async Task<IDomainValidationResult> CreateUserAsync(UserId userId, string email)
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                var user = new User(userId, email);

                _userRepository.Create(user);

                await _userRepository.UnitOfWork.CommitAsync();

                result.AddEntityToMetadata(user);
            }

            return result;
        }

        public async Task<IDomainValidationResult> UpdateUserAsync(User user, string email)
        {
            var result = new DomainValidationResult();

            if (result.IsValid)
            {
                user.Update(email);

                await _userRepository.UnitOfWork.CommitAsync();

                result.AddEntityToMetadata(user);
            }

            return result;
        }

        public async Task SendEmailAsync(UserId userId, string templateId, object templateData)
        {
            try
            {
                if (await this.UserExistsAsync(userId))
                {
                    var user = await _userRepository.FindUserAsync(userId);

                    await _emailService.SendEmailAsync(user.Email, templateId, templateData);
                }
                else
                {
                    _logger.LogCritical($"User doesn't exists. (userId=\"{userId}\")");
                }
            }
            catch (Exception exception)
            {
                _logger.LogError(exception, $"Failed to send email. (userId=\"{userId}\")");
            }
        }
    }
}
