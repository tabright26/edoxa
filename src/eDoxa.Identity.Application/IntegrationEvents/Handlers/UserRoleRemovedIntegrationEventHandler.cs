// Filename: UserRoleRemovedIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Application.IntegrationEvents.Handlers
{
    public class UserRoleRemovedIntegrationEventHandler : IIntegrationEventHandler<UserRoleRemovedIntegrationEvent>
    {
        private readonly UserManager<User> _userManager;

        public UserRoleRemovedIntegrationEventHandler(UserManager<User> userManager)
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