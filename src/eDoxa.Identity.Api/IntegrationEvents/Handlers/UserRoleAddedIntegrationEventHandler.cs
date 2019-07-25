// Filename: UserRoleAddedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Seedwork.IntegrationEvents;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public class UserRoleAddedIntegrationEventHandler : IIntegrationEventHandler<UserRoleAddedIntegrationEvent>
    {
        private readonly CustomUserManager _userManager;

        public UserRoleAddedIntegrationEventHandler(CustomUserManager userManager)
        {
            _userManager = userManager;
        }

        public async Task Handle(UserRoleAddedIntegrationEvent integrationEvent)
        {
            var user = await _userManager.FindByIdAsync(integrationEvent.UserId.ToString());

            if (!await _userManager.IsInRoleAsync(user, integrationEvent.RoleName))
            {
                await _userManager.AddToRoleAsync(user, integrationEvent.RoleName);
            }
        }
    }
}
