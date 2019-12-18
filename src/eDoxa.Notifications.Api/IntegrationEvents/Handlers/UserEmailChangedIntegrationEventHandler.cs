// Filename: UserEmailChangedIntegrationEventHandler.cs
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
    public sealed class UserEmailChangedIntegrationEventHandler : IIntegrationEventHandler<UserEmailChangedIntegrationEvent>
    {
        private readonly IUserService _userService;
        private readonly ILogger _logger;

        public UserEmailChangedIntegrationEventHandler(IUserService userService, ILogger<UserEmailChangedIntegrationEventHandler> logger)
        {
            _userService = userService;
            _logger = logger;
        }

        public async Task HandleAsync(UserEmailChangedIntegrationEvent integrationEvent)
        {
            var userId = integrationEvent.UserId.ParseEntityId<UserId>();

            if (await _userService.UserExistsAsync(userId))
            {
                var user = await _userService.FindUserAsync(userId);

                var result = await _userService.UpdateUserAsync(user, integrationEvent.Email.Address);

                if (result.IsValid)
                {
                    _logger.LogInformation(""); // FRANCIS: TODO.
                }
                else
                {
                    _logger.LogError(""); // FRANCIS: TODO.
                }
            }
            else
            {
                _logger.LogCritical(""); // FRANCIS: TODO.
            }
        }
    }
}
