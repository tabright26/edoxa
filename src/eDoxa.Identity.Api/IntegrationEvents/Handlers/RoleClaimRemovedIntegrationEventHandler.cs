﻿// Filename: RoleClaimRemovedIntegrationEventHandler.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.IntegrationEvents;
using eDoxa.Seedwork.Security;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public class RoleClaimRemovedIntegrationEventHandler : IIntegrationEventHandler<RoleClaimRemovedIntegrationEvent>
    {
        private readonly CustomRoleManager _roleManager;

        public RoleClaimRemovedIntegrationEventHandler(CustomRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task Handle(RoleClaimRemovedIntegrationEvent integrationEvent)
        {
            if (await _roleManager.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleManager.FindByNameAsync(integrationEvent.RoleName);

                await _roleManager.RemoveClaimAsync(role, new Claim(integrationEvent.ClaimType, integrationEvent.ClaimValue));
            }
        }
    }
}
