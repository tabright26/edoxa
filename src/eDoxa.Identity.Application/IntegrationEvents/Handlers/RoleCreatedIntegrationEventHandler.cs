// Filename: RoleCreatedIntegrationEventHandler.cs
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
    public class RoleCreatedIntegrationEventHandler : IIntegrationEventHandler<RoleCreatedIntegrationEvent>
    {
        private readonly RoleManager<Role> _roleManager;

        public RoleCreatedIntegrationEventHandler(RoleManager<Role> roleManager)
        {
            _roleManager = roleManager;
        }

        public async Task Handle(RoleCreatedIntegrationEvent integrationEvent)
        {
            if (!await _roleManager.RoleExistsAsync(integrationEvent.RoleName))
            {
                await _roleManager.CreateAsync(new Role(integrationEvent.RoleName));
            }
        }
    }
}