// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            subscriber.Subscribe<RoleClaimsAddedIntegrationEvent, RoleClaimsAddedIntegrationEventHandler>();
            subscriber.Subscribe<RoleClaimsRemovedIntegrationEvent, RoleClaimsRemovedIntegrationEventHandler>();
            subscriber.Subscribe<RoleCreatedIntegrationEvent, RoleCreatedIntegrationEventHandler>();
            subscriber.Subscribe<RoleDeletedIntegrationEvent, RoleDeletedIntegrationEventHandler>();
            subscriber.Subscribe<UserClaimsAddedIntegrationEvent, UserClaimsAddedIntegrationEventHandler>();
            subscriber.Subscribe<UserClaimsRemovedIntegrationEvent, UserClaimsRemovedIntegrationEventHandler>();
            subscriber.Subscribe<UserClaimsReplacedIntegrationEvent, UserClaimsReplacedIntegrationEventHandler>();
            subscriber.Subscribe<UserEmailSentIntegrationEvent, UserEmailSentIntegrationEventHandler>();
            subscriber.Subscribe<UserRoleAddedIntegrationEvent, UserRoleAddedIntegrationEventHandler>();
            subscriber.Subscribe<UserRoleRemovedIntegrationEvent, UserRoleRemovedIntegrationEventHandler>();
        }
    }
}
