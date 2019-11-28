// Filename: RoleClaimsAddedIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class RoleClaimsAddedIntegrationEventHandler : IIntegrationEventHandler<RoleClaimsAddedIntegrationEvent>
    {
        private readonly IRoleManager _roleManager;

        public RoleClaimsAddedIntegrationEventHandler(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task HandleAsync(RoleClaimsAddedIntegrationEvent integrationEvent)
        {
            if (await _roleManager.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleManager.FindByNameAsync(integrationEvent.RoleName);

                foreach (var claim in integrationEvent.Claims)
                {
                    await _roleManager.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
                }
            }
        }
    }
}
