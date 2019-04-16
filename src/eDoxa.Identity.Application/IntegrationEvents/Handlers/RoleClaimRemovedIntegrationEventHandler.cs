// Filename: RoleClaimRemovedIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.ServiceBus;

namespace eDoxa.Identity.Application.IntegrationEvents.Handlers
{
    public class RoleClaimRemovedIntegrationEventHandler : IIntegrationEventHandler<RoleClaimRemovedIntegrationEvent>
    {
        private readonly RoleService _roleService;

        public RoleClaimRemovedIntegrationEventHandler(RoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task Handle(RoleClaimRemovedIntegrationEvent integrationEvent)
        {
            if (await _roleService.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleService.FindByNameAsync(integrationEvent.RoleName);

                await _roleService.RemoveClaimAsync(role, new Claim(integrationEvent.ClaimType, integrationEvent.ClaimValue));
            }
        }
    }
}