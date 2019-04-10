// Filename: RoleCreatedIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Application.Services;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.ServiceBus;

namespace eDoxa.Identity.Application.IntegrationEvents.Handlers
{
    public class RoleCreatedIntegrationEventHandler : IIntegrationEventHandler<RoleCreatedIntegrationEvent>
    {
        private readonly RoleService _roleService;

        public RoleCreatedIntegrationEventHandler(RoleService roleService)
        {
            _roleService = roleService ?? throw new ArgumentNullException(nameof(roleService));
        }

        public async Task Handle(RoleCreatedIntegrationEvent integrationEvent)
        {
            if (!await _roleService.RoleExistsAsync(integrationEvent.RoleName))
            {
                await _roleService.CreateAsync(new Role(integrationEvent.RoleName));
            }
        }
    }
}