// Filename: ISubscriptionCollection.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

namespace eDoxa.Seedwork.IntegrationEvents.Infrastructure
{
    public interface IIntegrationEventSubscriptionStore
    {
        bool IsEmpty { get; }

        event EventHandler<string> OnIntegrationEventRemoved;

        void Clear();

        bool Contains(string integrationEventTypeName);

        bool Contains<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent;

        string GetIntegrationEventTypeName<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent;

        Type? TryGetIntegrationEventType(string integrationEventTypeName);

        IReadOnlyCollection<IntegrationEventSubscription> FetchSubscriptions(string integrationEventTypeName);

        IReadOnlyCollection<IntegrationEventSubscription> FetchSubscriptions<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent;

        bool TryGetSubscription(string integrationEventTypeName, Type integrationEventHandlerType, out IntegrationEventSubscription integrationEventSubscription);

        bool TryGetSubscription<TIntegrationEvent, TIntegrationEventHandler>(out IntegrationEventSubscription integrationEventSubscription)
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        bool TryGetSubscription<TDynamicIntegrationEventHandler>(string integrationEventTypeName, out IntegrationEventSubscription integrationEventSubscription)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;

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
