// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseServiceBusSubscriber(this IApplicationBuilder application)
        {
            var serviceBusSubscriber = application.ApplicationServices.GetRequiredService<IServiceBusSubscriber>();
            serviceBusSubscriber.Subscribe<RoleClaimAddedIntegrationEvent, RoleClaimAddedIntegrationEventHandler>();
            serviceBusSubscriber.Subscribe<RoleClaimRemovedIntegrationEvent, RoleClaimRemovedIntegrationEventHandler>();
            serviceBusSubscriber.Subscribe<RoleCreatedIntegrationEvent, RoleCreatedIntegrationEventHandler>();
            serviceBusSubscriber.Subscribe<RoleDeletedIntegrationEvent, RoleDeletedIntegrationEventHandler>();
            serviceBusSubscriber.Subscribe<UserClaimsAddedIntegrationEvent, UserClaimsAddedIntegrationEventHandler>();
            serviceBusSubscriber.Subscribe<UserClaimsRemovedIntegrationEvent, UserClaimsRemovedIntegrationEventHandler>();
            serviceBusSubscriber.Subscribe<UserClaimsReplacedIntegrationEvent, UserClaimsReplacedIntegrationEventHandler>();
            serviceBusSubscriber.Subscribe<UserRoleAddedIntegrationEvent, UserRoleAddedIntegrationEventHandler>();
            serviceBusSubscriber.Subscribe<UserRoleRemovedIntegrationEvent, UserRoleRemovedIntegrationEventHandler>();
        }
    }
}
