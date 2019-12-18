// Filename: UserCreatedIntegrationEventHandler.cs
// Date Created: 2019-12-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Notifications.Domain.Services;
using eDoxa.Seedwork.Domain.Extensions;
using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Api.IntegrationEvents.Handlers
{
    public sealed class UserCreatedIntegrationEventHandler : IIntegrationEventHandler<UserCreatedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserCreatedIntegrationEventHandler(IUserService userService, ILogger<UserCreatedIntegrationEventHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(UserCreatedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            if (!await _userService.UserExistsAsync(userId))
            {
                var result = await _userService.CreateUserAsync(userId, integrationEvent.Email.Address);

                if (result.IsValid)
                {
                    await _userService.SendEmailAsync(
                        integrationEvent.UserId.ParseEntityId<UserId>(),
                        nameof(UserCreatedIntegrationEvent),
                        nameof(UserCreatedIntegrationEvent));
                }
                else
                {
                    _logger.LogCritical(""); // FRANCIS: TODO.
                }
            }
            else
            {
                _logger.LogWarning(""); // FRANCIS: TODO.
            }
        }
    }
}
