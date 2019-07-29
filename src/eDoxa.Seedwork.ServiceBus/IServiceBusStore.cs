// Filename: IServiceBusStore.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

namespace eDoxa.Seedwork.ServiceBus
{
    public interface IServiceBusStore
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnIntegrationEventRemoved;

        void Clear();

        bool Contains(string integrationEventTypeName);

        bool Contains<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent;

        bool TryGetTypeFor(string integrationEventTypeName, out Type integrationEventType);

        IReadOnlyCollection<Type> GetHandlerTypesFor(string integrationEventTypeName);

        IReadOnlyCollection<Type> GetHandlerTypesFor<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent;

        void AddSubscription<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        void AddSubscription<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;

        void RemoveSubscription<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        void RemoveSubscription<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;
    }
}
