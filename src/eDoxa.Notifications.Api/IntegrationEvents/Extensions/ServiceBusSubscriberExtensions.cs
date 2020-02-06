// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Grpc.Protos.Cashier.IntegrationEvents;
using eDoxa.Grpc.Protos.Challenges.IntegrationEvents;
using eDoxa.Grpc.Protos.Clans.IntegrationEvents;
using eDoxa.Grpc.Protos.Games.IntegrationEvents;
using eDoxa.Grpc.Protos.Identity.IntegrationEvents;
using eDoxa.Grpc.Protos.Payment.IntegrationEvents;
using eDoxa.Notifications.Api.IntegrationEvents.Handlers;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Notifications.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            subscriber.Subscribe<ChallengeClosedIntegrationEvent, ChallengeClosedIntegrationEventHandler>();
            subscriber.Subscribe<ClanCandidatureSentIntegrationEvent, ClanCandidatureSentIntegrationEventHandler>();
            subscriber.Subscribe<ClanInvitationSentIntegrationEvent, ClanInvitationSentIntegrationEventHandler>();
            subscriber.Subscribe<ClanMemberAddedIntegrationEvent, ClanMemberAddedIntegrationEventHandler>();
            subscriber.Subscribe<ClanMemberRemovedIntegrationEvent, ClanMemberRemovedIntegrationEventHandler>();
            subscriber.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
            subscriber.Subscribe<UserDepositFailedIntegrationEvent, UserDepositFailedIntegrationEventHandler>();
            subscriber.Subscribe<UserDepositSucceededIntegrationEvent, UserDepositSucceededIntegrationEventHandler>();
            subscriber.Subscribe<UserGameCredentialAddedIntegrationEvent, UserGameCredentialAddedIntegrationEventHandler>();
            subscriber.Subscribe<UserGameCredentialRemovedIntegrationEvent, UserGameCredentialRemovedIntegrationEventHandler>();
            subscriber.Subscribe<UserWithdrawalFailedIntegrationEvent, UserWithdrawalFailedIntegrationEventHandler>();
            subscriber.Subscribe<UserWithdrawalSucceededIntegrationEvent, UserWithdrawalSucceededIntegrationEventHandler>();
            subscriber.Subscribe<UserEmailConfirmationTokenGeneratedIntegrationEvent, UserEmailConfirmationTokenGeneratedIntegrationEventHandler>();
            subscriber.Subscribe<UserPasswordResetTokenGeneratedIntegrationEvent, UserPasswordResetTokenGeneratedIntegrationEventHandler>();
            subscriber.Subscribe<ChallengeParticipantRegisteredIntegrationEvent, ChallengeParticipantRegisteredIntegrationEventHandler>();
            subscriber.Subscribe<ChallengeStartedIntegrationEvent, ChallengeStartedntegrationEventHandler>();
        }
    }
}
