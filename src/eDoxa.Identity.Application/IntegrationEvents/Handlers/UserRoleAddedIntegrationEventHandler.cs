// Filename: UserRoleAddedIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.ServiceBus;

namespace eDoxa.Identity.Application.IntegrationEvents.Handlers
{
    public class UserRoleAddedIntegrationEventHandler : IIntegrationEventHandler<UserRoleAddedIntegrationEvent>
    {
        private readonly UserService _userService;

        public UserRoleAddedIntegrationEventHandler(UserService userService)
        {
            _userService = userService;
        }

        public async Task Handle(UserRoleAddedIntegrationEvent integrationEvent)
        {
            var user = await _userService.FindByIdAsync(integrationEvent.UserId.ToString());

            if (!await _userService.IsInRoleAsync(user, integrationEvent.RoleName))
            {
                await _userService.AddToRoleAsync(user, integrationEvent.RoleName);
            }
        }
    }
}