// Filename: IServiceBusPublisher.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public interface IServiceBusPublisher : IDisposable
    {
        void Publish(IntegrationEvent integrationEvent);

        void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        void Subscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;

        void Unsubscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        void Unsubscribe<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;
    }
}
