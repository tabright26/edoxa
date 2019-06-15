// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.IntegrationEvents;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Api.Extensions
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

            service.Subscribe<UserClaimReplacedIntegrationEvent, UserClaimReplacedIntegrationEventHandler>();

            service.Subscribe<UserRoleAddedIntegrationEvent, UserRoleAddedIntegrationEventHandler>();

            service.Subscribe<UserRoleRemovedIntegrationEvent, UserRoleRemovedIntegrationEventHandler>();
        }
    }
}
