// Filename: UserRoleRemovedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class UserRoleRemovedIntegrationEventHandler : IIntegrationEventHandler<UserRoleRemovedIntegrationEvent>
    {
        private readonly IUserManager _userManager;

        public UserRoleRemovedIntegrationEventHandler(IUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task HandleAsync(UserRoleRemovedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            if (await _userManager.IsInRoleAsync(user, integrationEvent.RoleName))
            {
                await _userManager.RemoveFromRoleAsync(user, integrationEvent.RoleName);
            }
        }
    }
}
