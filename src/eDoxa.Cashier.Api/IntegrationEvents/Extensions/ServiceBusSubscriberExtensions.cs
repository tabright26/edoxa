// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            // Identity service integration events.
            subscriber.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
            subscriber.Subscribe<UserTransactionSuccededIntegrationEvent, UserTransactionSuccededIntegrationEventHandler>();
            subscriber.Subscribe<UserTransactionFailedIntegrationEvent, UserTransactionFailedIntegrationEventHandler>();

            // Challenge service integration events.
            subscriber.Subscribe<ChallengeDeletedIntegrationEvent, ChallengeDeletedIntegrationEventHandler>();
            subscriber.Subscribe<ChallengeClosedIntegrationEvent, ChallengeClosedIntegrationEventHandler>();
        }
    }
}
