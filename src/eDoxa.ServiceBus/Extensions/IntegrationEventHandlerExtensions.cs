// Filename: IntegrationEventHandlerExtensions.cs
// Date Created: 2019-05-04
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

namespace eDoxa.ServiceBus.Extensions
{
    public static class IntegrationEventHandlerExtensions
    {
        public static async Task HandleAsync<TIntegrationEvent>(this IIntegrationEventHandler<TIntegrationEvent> handler, TIntegrationEvent integrationEvent)
        where TIntegrationEvent : IntegrationEvent
        {
            await handler.Handle(integrationEvent);
        }
    }
}