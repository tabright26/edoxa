// Filename: RoleDeletedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Areas.Identity.Services;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    internal sealed class RoleDeletedIntegrationEventHandler : IIntegrationEventHandler<RoleDeletedIntegrationEvent>
    {
        private readonly RoleManager _roleManager;

        public RoleDeletedIntegrationEventHandler(RoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task HandleAsync(RoleDeletedIntegrationEvent integrationEvent)
        {
            if (await _roleManager.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleManager.FindByNameAsync(integrationEvent.RoleName);

                await _roleManager.DeleteAsync(role);
            }
        }
    }
}
