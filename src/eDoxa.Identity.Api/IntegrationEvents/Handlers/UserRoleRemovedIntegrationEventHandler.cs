// Filename: UserRoleRemovedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Managers;
using eDoxa.IntegrationEvents;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public class UserRoleRemovedIntegrationEventHandler : IIntegrationEventHandler<UserRoleRemovedIntegrationEvent>
    {
        private readonly CustomUserManager _userManager;

        public UserRoleRemovedIntegrationEventHandler(CustomUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UserRoleRemovedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            if (await _userManager.IsInRoleAsync(user, integrationEvent.RoleName))
            {
                await _userManager.RemoveFromRoleAsync(user, integrationEvent.RoleName);
            }
        }
    }
}
