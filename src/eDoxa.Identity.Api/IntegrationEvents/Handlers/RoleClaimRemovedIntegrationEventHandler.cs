// Filename: RoleClaimRemovedIntegrationEventHandler.cs
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

using eDoxa.Identity.Infrastructure.Models;
using eDoxa.IntegrationEvents;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public class RoleClaimRemovedIntegrationEventHandler : IIntegrationEventHandler<RoleClaimRemovedIntegrationEvent>
    {
        private readonly RoleManager<RoleModel> _roleManager;

        public RoleClaimRemovedIntegrationEventHandler(RoleManager<RoleModel> roleManager)
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
