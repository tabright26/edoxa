// Filename: UserRoleAddedIntegrationEventHandler.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Security;

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
