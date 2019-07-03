// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents;
using eDoxa.Payment.Api.IntegrationEvents.Handlers;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Payment.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IApplicationBuilder application)
        {
            var service = application.ApplicationServices.GetRequiredService<IEventBusService>();

            service.Subscribe<PaymentDepositedIntegrationEvent, PaymentDepositedIntegrationEventHandler>();

            service.Subscribe<PaymentWithdrawnIntegrationEvent, PaymentWithdrawnIntegrationEventHandler>();
        }
    }
}
