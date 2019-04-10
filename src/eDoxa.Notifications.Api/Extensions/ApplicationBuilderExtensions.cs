// Filename: NotificationsExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Notifications.Application.IntegrationEvents;
using eDoxa.Notifications.Application.IntegrationEvents.Handlers;
using eDoxa.ServiceBus;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Notifications.Api.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseIntegrationEventSubscriptions(this IApplicationBuilder application)
        {
            var service = application.ApplicationServices.GetRequiredService<IEventBusService>();

            service.Subscribe<UserCreatedIntegrationEvent, UserCreatedIntegrationEventHandler>();

            service.Subscribe<UserNotifiedIntegrationEvent, UserNotifiedIntegrationEventHandler>();
        }
    }
}