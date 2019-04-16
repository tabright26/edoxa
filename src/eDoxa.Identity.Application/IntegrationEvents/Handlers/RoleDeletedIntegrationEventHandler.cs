// Filename: RoleDeletedIntegrationEventHandler.cs
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
    public class RoleDeletedIntegrationEventHandler : IIntegrationEventHandler<RoleDeletedIntegrationEvent>
    {
        private readonly RoleService _roleService;

        public RoleDeletedIntegrationEventHandler(RoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task Handle(RoleDeletedIntegrationEvent integrationEvent)
        {
            if (await _roleService.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleService.FindByNameAsync(integrationEvent.RoleName);

                await _roleService.DeleteAsync(role);
            }
        }
    }
}