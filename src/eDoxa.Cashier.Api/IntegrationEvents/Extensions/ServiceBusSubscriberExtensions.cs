// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Cashier.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            subscriber.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
            subscriber.Subscribe<UserStripePaymentIntentSucceededIntegrationEvent, UserStripePaymentIntentSucceededIntegrationEventHandler>();
            subscriber.Subscribe<UserStripePaymentIntentPaymentFailedIntegrationEvent, UserStripePaymentIntentPaymentFailedIntegrationEventHandler>();
            subscriber.Subscribe<UserStripePaymentIntentCanceledIntegrationEvent, UserStripePaymentIntentCanceledIntegrationEventHandler>();
            subscriber.Subscribe<UserWithdrawSucceededIntegrationEvent, UserWithdrawSucceededIntegrationEventHandler>();
            subscriber.Subscribe<UserWithdrawFailedIntegrationEvent, UserWithdrawFailedIntegrationEventHandler>();
            subscriber.Subscribe<ChallengeSynchronizedIntegrationEvent, ChallengeSynchronizedIntegrationEventHandler>();
            subscriber.Subscribe<ChallengeParticipantRegisteredIntegrationEvent, ChallengeParticipantRegisteredIntegrationEventHandler>();
            subscriber.Subscribe<RegisterChallengeParticipantFailedIntegrationEvent, RegisterChallengeParticipantFailedIntegrationEventHandler>();
        }
    }
}
