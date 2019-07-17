// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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
            var eventBusService = application.ApplicationServices.GetRequiredService<IEventBusService>();
            eventBusService.Subscribe<RoleClaimAddedIntegrationEvent, RoleClaimAddedIntegrationEventHandler>();
            eventBusService.Subscribe<RoleClaimRemovedIntegrationEvent, RoleClaimRemovedIntegrationEventHandler>();
            eventBusService.Subscribe<RoleCreatedIntegrationEvent, RoleCreatedIntegrationEventHandler>();
            eventBusService.Subscribe<RoleDeletedIntegrationEvent, RoleDeletedIntegrationEventHandler>();
            eventBusService.Subscribe<UserClaimsAddedIntegrationEvent, UserClaimsAddedIntegrationEventHandler>();
            eventBusService.Subscribe<UserClaimsRemovedIntegrationEvent, UserClaimsRemovedIntegrationEventHandler>();
            eventBusService.Subscribe<UserClaimsReplacedIntegrationEvent, UserClaimsReplacedIntegrationEventHandler>();
            eventBusService.Subscribe<UserRoleAddedIntegrationEvent, UserRoleAddedIntegrationEventHandler>();
            eventBusService.Subscribe<UserRoleRemovedIntegrationEvent, UserRoleRemovedIntegrationEventHandler>();
        }
    }
}
