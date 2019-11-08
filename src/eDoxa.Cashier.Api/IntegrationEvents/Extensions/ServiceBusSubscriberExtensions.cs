// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            subscriber.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
            subscriber.Subscribe<UserTransactionSuccededIntegrationEvent, UserTransactionSuccededIntegrationEventHandler>();
            subscriber.Subscribe<UserTransactionFailedIntegrationEvent, UserTransactionFailedIntegrationEventHandler>();
            subscriber.Subscribe<ChallengeCreationFailedIntegrationEvent, ChallengeCreationFailedIntegrationEventHandler>();
        }
    }
}
