// Filename: RoleDeletedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Identity.Api.Application.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class RoleDeletedIntegrationEventHandler : IIntegrationEventHandler<RoleDeletedIntegrationEvent>
    {
        private readonly IRoleService _roleService;

        public RoleDeletedIntegrationEventHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task HandleAsync(RoleDeletedIntegrationEvent integrationEvent)
        {
            if (await _roleService.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleService.FindByNameAsync(integrationEvent.RoleName);

                await _roleService.DeleteAsync(role);
            }
        }
    }
}
