// Filename: ServiceBusSubscriberExtensions.cs
// Date Created: 2019-10-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.ServiceBus.Abstractions;

namespace eDoxa.Payment.Api.IntegrationEvents.Extensions
{
    public static class ServiceBusSubscriberExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IServiceBusSubscriber subscriber)
        {
            subscriber.Subscribe<UserAccountDepositIntegrationEvent, UserAccountDepositIntegrationEventHandler>();
            subscriber.Subscribe<UserAccountWithdrawalIntegrationEvent, UserAccountWithdrawalIntegrationEventHandler>();
        }
    }
}
