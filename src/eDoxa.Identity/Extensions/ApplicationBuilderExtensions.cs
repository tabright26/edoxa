// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-04-01
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Application.IntegrationEvents;
using eDoxa.Identity.Application.IntegrationEvents.Handlers;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IApplicationBuilder application)
        {
            var service = application.ApplicationServices.GetRequiredService<IEventBusService>();

            service.Subscribe<RoleClaimAddedIntegrationEvent, RoleClaimAddedIntegrationEventHandler>();

            service.Subscribe<RoleClaimRemovedIntegrationEvent, RoleClaimRemovedIntegrationEventHandler>();

            service.Subscribe<RoleCreatedIntegrationEvent, RoleCreatedIntegrationEventHandler>();

            service.Subscribe<RoleDeletedIntegrationEvent, RoleDeletedIntegrationEventHandler>();

            service.Subscribe<UserClaimAddedIntegrationEvent, UserClaimAddedIntegrationEventHandler>();

            service.Subscribe<UserClaimRemovedIntegrationEvent, UserClaimRemovedIntegrationEventHandler>();

            service.Subscribe<UserRoleAddedIntegrationEvent, UserRoleAddedIntegrationEventHandler>();

            service.Subscribe<UserRoleRemovedIntegrationEvent, UserRoleRemovedIntegrationEventHandler>();
        }
    }
}