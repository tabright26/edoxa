// Filename: RoleDeletedIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Identity;

namespace eDoxa.Identity.Application.IntegrationEvents.Handlers
{
    public class RoleDeletedIntegrationEventHandler : IIntegrationEventHandler<RoleDeletedIntegrationEvent>
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleDeletedIntegrationEventHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task Handle(RoleDeletedIntegrationEvent integrationEvent)
        {
            if (await _roleManager.RoleExistsAsync(integrationEvent.RoleName))
            {
                var role = await _roleManager.FindByNameAsync(integrationEvent.RoleName);

                await _roleManager.DeleteAsync(role);
            }
        }
    }
}