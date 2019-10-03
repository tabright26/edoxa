// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;
using eDoxa.ServiceBus.Abstractions;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Payment.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseServiceBusSubscriber(this IApplicationBuilder application)
        {
            var serviceBusSubscriber = application.ApplicationServices.GetRequiredService<IServiceBusSubscriber>();
            serviceBusSubscriber.Subscribe<UserAccountDepositIntegrationEvent, UserAccountDepositIntegrationEventHandler>();
            serviceBusSubscriber.Subscribe<UserAccountWithdrawalIntegrationEvent, UserAccountWithdrawalIntegrationEventHandler>();
        }
    }
}
