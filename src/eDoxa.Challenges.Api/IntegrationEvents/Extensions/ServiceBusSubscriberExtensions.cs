// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Api.IntegrationEvents.Handlers;
using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            subscriber.Subscribe<ChallengeClosedIntegrationEvent, ChallengeClosedIntegrationEventHandler>();
            subscriber.Subscribe<CreateChallengePayoutFailedIntegrationEvent, CreateChallengePayoutFailedIntegrationEventHandler>();
        }
    }
}
