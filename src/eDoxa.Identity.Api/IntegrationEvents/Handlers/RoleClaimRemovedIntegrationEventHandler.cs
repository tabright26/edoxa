// Filename: RoleClaimRemovedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public class RoleClaimRemovedIntegrationEventHandler : IIntegrationEventHandler<RoleClaimRemovedIntegrationEvent>
    {
        private readonly CustomRoleManager _roleManager;

        public RoleClaimRemovedIntegrationEventHandler(CustomRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task HandleAsync(RoleClaimRemovedIntegrationEvent integrationEvent)
        {
            if (await _roleManager.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleManager.FindByNameAsync(integrationEvent.RoleName);

                await _roleManager.RemoveClaimAsync(role, new Claim(integrationEvent.ClaimType, integrationEvent.ClaimValue));
            }
        }
    }
}
