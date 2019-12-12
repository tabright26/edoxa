// Filename: RoleClaimsAddedIntegrationEventHandler.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Security.Claims;
using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class RoleClaimsAddedIntegrationEventHandler : IIntegrationEventHandler<RoleClaimsAddedIntegrationEvent>
    {
        private readonly IRoleService _roleService;

        public RoleClaimsAddedIntegrationEventHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task HandleAsync(RoleClaimsAddedIntegrationEvent integrationEvent)
        {
            if (await _roleService.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleService.FindByNameAsync(integrationEvent.RoleName);

                foreach (var claim in integrationEvent.Claims)
                {
                    await _roleService.AddClaimAsync(role, new Claim(claim.Type, claim.Value));
                }
            }
        }
    }
}
