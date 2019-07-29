// Filename: InMemoryServiceBusStore.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public sealed class InMemoryServiceBusStore : IServiceBusStore
    {
        private readonly HashSet<Type> _integrationEventTypes = new HashSet<Type>();
        private readonly Dictionary<string, HashSet<Type>> _integrationEventHandlers = new Dictionary<string, HashSet<Type>>();

        public event EventHandler<string> OnIntegrationEventRemoved;

        public bool IsEmpty => !_integrationEventHandlers.Any();

        public void Clear()
        {
            _integrationEventHandlers.Clear();
        }

        public bool Contains(string integrationEventTypeName)
        {
            return _integrationEventHandlers.ContainsKey(integrationEventTypeName);
        }

        public bool Contains<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent
        {
            return this.Contains(typeof(TIntegrationEvent).Name);
        }

        public IReadOnlyCollection<Type> GetHandlerTypesFor(string integrationEventTypeName)
        {
            return _integrationEventHandlers.TryGetValue(integrationEventTypeName, out var subscriptions) ? subscriptions.ToHashSet() : new HashSet<Type>();
        }

        public IReadOnlyCollection<Type> GetHandlerTypesFor<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent
        {
            return this.GetHandlerTypesFor(typeof(TIntegrationEvent).Name);
        }

        public bool TryGetTypeFor(string integrationEventTypeName, out Type integrationEventType)
        {
            integrationEventType = _integrationEventTypes.SingleOrDefault(type => type.Name == integrationEventTypeName) ?? typeof(IntegrationEvent);

            return integrationEventType != typeof(IntegrationEvent);
        }

        public void AddSubscription<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            this.AddSubscription(typeof(TIntegrationEvent).Name, typeof(TIntegrationEventHandler));

            _integrationEventTypes.Add(typeof(TIntegrationEvent));
        }

        public void AddSubscription<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            this.AddSubscription(integrationEventTypeName, typeof(TDynamicIntegrationEventHandler));
        }

        public void RemoveSubscription<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            if (this.TryGetHandlerTypeFor<TIntegrationEvent, TIntegrationEventHandler>(out var handlerType))
            {
                this.RemoveSubscription(typeof(TIntegrationEvent).Name, handlerType);
            }
        }

        public void RemoveSubscription<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            if (this.TryGetHandlerTypeFor<TDynamicIntegrationEventHandler>(integrationEventTypeName, out var handlerType))
            {
                this.RemoveSubscription(integrationEventTypeName, handlerType);
            }
        }

        public bool TryGetHandlerTypeFor(string integrationEventTypeName, Type integrationEventHandlerType, out Type handlerType)
        {
            handlerType = typeof(Type);

            if (_integrationEventHandlers.TryGetValue(integrationEventTypeName, out var handlerTypes))
            {
                handlerType = handlerTypes?.SingleOrDefault(type => type == integrationEventHandlerType) ?? handlerType;
            }

            return handlerType != typeof(Type);
        }

        public bool TryGetHandlerTypeFor<TIntegrationEvent, TDynamicIntegrationEventHandler>(out Type handlerType)
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            return this.TryGetHandlerTypeFor(typeof(TIntegrationEvent).Name, typeof(TDynamicIntegrationEventHandler), out handlerType);
        }

        public bool TryGetHandlerTypeFor<TDynamicIntegrationEventHandler>(string integrationEventTypeName, out Type handlerType)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            return this.TryGetHandlerTypeFor(integrationEventTypeName, typeof(TDynamicIntegrationEventHandler), out handlerType);
        }

        private void AddSubscription(string integrationEventTypeName, Type handlerType)
        {
            if (!_integrationEventHandlers.ContainsKey(integrationEventTypeName))
            {
                _integrationEventHandlers.Add(
                    integrationEventTypeName,
                    new HashSet<Type>
                    {
                        handlerType
                    }
                );
            }
            else
            {
                if (!_integrationEventHandlers[integrationEventTypeName].Add(handlerType))
                {
                    throw new InvalidOperationException($"The {handlerType} has already subscribed to {integrationEventTypeName}.");
                }
            }
        }

        private void RemoveSubscription(string integrationEventTypeName, Type handlerType)
        {
            _integrationEventHandlers[integrationEventTypeName].Remove(handlerType);

            if (_integrationEventHandlers[integrationEventTypeName].Any())
            {
                return;
            }

            _integrationEventHandlers.Remove(integrationEventTypeName);

            var integrationEventType = _integrationEventTypes.SingleOrDefault(integrationEvent => integrationEvent.Name == integrationEventTypeName);

            if (integrationEventType != null)
            {
                _integrationEventTypes.Remove(integrationEventType);
            }

            OnIntegrationEventRemoved?.Invoke(this, integrationEventTypeName);
        }
    }
}
