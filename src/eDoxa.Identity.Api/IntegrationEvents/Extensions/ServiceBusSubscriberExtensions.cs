// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Grpc.Protos.Games.IntegrationEvents;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Identity.Api.IntegrationEvents.Handlers;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Identity.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            subscriber.Subscribe<ClanMemberAddedIntegrationEvent, ClanMemberAddedIntegrationEventHandler>();
            subscriber.Subscribe<ClanMemberRemovedIntegrationEvent, ClanMemberRemovedIntegrationEventHandler>();
            subscriber.Subscribe<UserGameCredentialAddedIntegrationEvent, UserGameCredentialAddedIntegrationEventHandler>();
            subscriber.Subscribe<UserGameCredentialRemovedIntegrationEvent, UserGameCredentialRemovedIntegrationEventHandler>();
            subscriber.Subscribe<UserStripeCustomerCreatedIntegrationEvent, UserStripeCustomerCreatedIntegrationEventHandler>();
        }
    }
}
