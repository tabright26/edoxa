// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Api.IntegrationEvents.Handlers;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Challenges.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            subscriber.Subscribe<ChallengeDeletedIntegrationEvent, ChallengeDeletedIntegrationEventHandler>();
            subscriber.Subscribe<ChallengesSynchronizedIntegrationEvent, ChallengesSynchronizedIntegrationEventHandler>();
        }
    }
}
