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
        private readonly IRoleManager _roleManager;

        public RoleCreatedIntegrationEventHandler(IRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task HandleAsync(RoleCreatedIntegrationEvent integrationEvent)
        {
            if (!await _roleManager.RoleExistsAsync(integrationEvent.RoleName))
            {
                await _roleManager.CreateAsync(
                    new Role
                    {
                        Name = integrationEvent.RoleName
                    }
                );
            }
        }
    }
}
