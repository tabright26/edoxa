// Filename: RoleCreatedIntegrationEventHandler.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Application.Managers;
using eDoxa.Identity.Api.Models;
using eDoxa.IntegrationEvents;

namespace eDoxa.Identity.Api.IntegrationEvents.Handlers
{
    public class RoleCreatedIntegrationEventHandler : IIntegrationEventHandler<RoleCreatedIntegrationEvent>
    {
        private readonly CustomRoleManager _roleManager;

        public RoleCreatedIntegrationEventHandler(CustomRoleManager roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task Handle(RoleCreatedIntegrationEvent integrationEvent)
        {
            if (!await _roleManager.RoleExistsAsync(integrationEvent.RoleName))
            {
                await _roleManager.CreateAsync(
                    new RoleModel
                    {
                        Name = integrationEvent.RoleName
                    }
                );
            }
        }
    }
}
