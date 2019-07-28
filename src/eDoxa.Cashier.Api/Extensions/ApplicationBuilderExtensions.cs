// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api.IntegrationEvents;
using eDoxa.Cashier.Api.IntegrationEvents.Handlers;
using eDoxa.Seedwork.IntegrationEvents;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Cashier.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IApplicationBuilder application)
        {
            var service = application.ApplicationServices.GetRequiredService<IServiceBusPublisher>();
            service.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();
            service.Subscribe<TransactionSuccededIntegrationEvent, TransactionSuccededIntegrationEventHandler>();
            service.Subscribe<TransactionFailedIntegrationEvent, TransactionFailedIntegrationEventHandler>();
        }
    }
}
