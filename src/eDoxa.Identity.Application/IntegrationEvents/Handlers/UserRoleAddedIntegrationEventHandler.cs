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

using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Application.IntegrationEvents.Handlers
{
    public class UserRoleAddedIntegrationEventHandler : IIntegrationEventHandler<UserRoleAddedIntegrationEvent>
    {
        private readonly UserManager<User> _userManager;

        public UserRoleAddedIntegrationEventHandler(UserManager<User> userManager)
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