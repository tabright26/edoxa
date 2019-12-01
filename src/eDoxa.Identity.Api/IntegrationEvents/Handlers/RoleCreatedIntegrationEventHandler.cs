// Filename: RoleCreatedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;
using eDoxa.Identity.Api.Services;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public sealed class RoleCreatedIntegrationEventHandler : IIntegrationEventHandler<RoleCreatedIntegrationEvent>
    {
        private readonly IRoleService _roleService;

        public RoleCreatedIntegrationEventHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }

        public async Task HandleAsync(RoleCreatedIntegrationEvent integrationEvent)
        {
            if (!await _roleService.RoleExistsAsync(integrationEvent.RoleName))
            {
                await _roleService.CreateAsync(
                    new Role
                    {
                        Name = integrationEvent.RoleName
                    }
                );
            }
        }
    }
}
