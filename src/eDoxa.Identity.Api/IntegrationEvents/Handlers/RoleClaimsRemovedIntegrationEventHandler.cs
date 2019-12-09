// Filename: RoleClaimsRemovedIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class RoleClaimsRemovedIntegrationEventHandler : IIntegrationEventHandler<RoleClaimsRemovedIntegrationEvent>
    {
        private readonly IRoleService _roleService;

        public RoleClaimsRemovedIntegrationEventHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task HandleAsync(RoleClaimsRemovedIntegrationEvent integrationEvent)
        {
            if (await _roleService.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleService.FindByNameAsync(integrationEvent.RoleName);

                foreach (var claim in integrationEvent.Claims)
                {
                    await _roleService.RemoveClaimAsync(role, new Claim(claim.Type, claim.Value));
                }
            }
        }
    }
}
