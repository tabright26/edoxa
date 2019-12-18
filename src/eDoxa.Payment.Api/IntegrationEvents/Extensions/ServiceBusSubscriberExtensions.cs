// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            subscriber.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
            subscriber.Subscribe<UserEmailChangedIntegrationEvent, UserEmailChangedIntegrationEventHandler>();
            subscriber.Subscribe<UserPhoneChangedIntegrationEvent, UserPhoneChangedIntegrationEventHandler>();
            subscriber.Subscribe<UserAddressChangedIntegrationEvent, UserAddressChangedIntegrationEventHandler>();
            subscriber.Subscribe<UserProfileChangedIntegrationEvent, UserProfileChangedIntegrationEventHandler>();
        }
    }
}
