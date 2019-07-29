// Filename: RoleClaimAddedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.Seedwork.ServiceBus;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public class RoleClaimAddedIntegrationEventHandler : IIntegrationEventHandler<RoleClaimAddedIntegrationEvent>
    {
        private readonly CustomRoleManager _roleManager;

        public RoleClaimAddedIntegrationEventHandler(CustomRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task HandleAsync(RoleClaimAddedIntegrationEvent integrationEvent)
        {
            if (await _roleManager.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleManager.FindByNameAsync(integrationEvent.RoleName);

                await _roleManager.AddClaimAsync(role, new Claim(integrationEvent.ClaimType, integrationEvent.ClaimValue));
            }
        }
    }
}
