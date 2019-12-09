// Filename: UserRoleRemovedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserRoleRemovedIntegrationEventHandler : IIntegrationEventHandler<UserRoleRemovedIntegrationEvent>
    {
        private readonly IUserService _userService;

        public UserRoleRemovedIntegrationEventHandler(IUserService userService)
        {
            _userService = userService;
        }

        public async Task HandleAsync(UserRoleRemovedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId.ToString());

            if (await _userService.IsInRoleAsync(user, integrationEvent.RoleName))
            {
                await _userService.RemoveFromRoleAsync(user, integrationEvent.RoleName);
            }
        }
    }
}
