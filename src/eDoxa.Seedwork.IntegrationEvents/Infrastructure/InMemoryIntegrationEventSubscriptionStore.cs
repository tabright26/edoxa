// Filename: InMemoryIntegrationEventSubscriptionManager.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Seedwork.IntegrationEvents.Infrastructure
{
    public sealed class InMemoryIntegrationEventSubscriptionStore : IIntegrationEventSubscriptionStore
    {
        private readonly HashSet<Type> _integrationEventTypes = new HashSet<Type>();
        private readonly Dictionary<string, HashSet<IntegrationEventSubscription>> _integrationEventHandlers =
            new Dictionary<string, HashSet<IntegrationEventSubscription>>();

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
            return this.Contains(this.GetIntegrationEventTypeName<TIntegrationEvent>());
        }

        public bool TryGetSubscription(
            string integrationEventTypeName,
            Type integrationEventHandlerType,
            out IntegrationEventSubscription integrationEventSubscription
        )
        {
            integrationEventSubscription = IntegrationEventSubscription.Default;

            if (_integrationEventHandlers.TryGetValue(integrationEventTypeName, out var integrationEventSubscriptions))
            {
                integrationEventSubscription =
                    integrationEventSubscriptions?.SingleOrDefault(subscription => subscription.HandlerType == integrationEventHandlerType) ??
                    integrationEventSubscription;
            }

            return integrationEventSubscription != IntegrationEventSubscription.Default;
        }

        public bool TryGetSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>(out IntegrationEventSubscription integrationEventSubscription)
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            return this.TryGetSubscription(
                this.GetIntegrationEventTypeName<TIntegrationEvent>(),
                typeof(TDynamicIntegrationEventHandler),
                out integrationEventSubscription
            );
        }

        public bool TryGetSubscription<TDynamicIntegrationEventHandler>(
            string integrationEventTypeName,
            out IntegrationEventSubscription integrationEventSubscription
        )
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            return this.TryGetSubscription(integrationEventTypeName, typeof(TDynamicIntegrationEventHandler), out integrationEventSubscription);
        }

        public IReadOnlyCollection<IntegrationEventSubscription> FetchSubscriptions(string integrationEventTypeName)
        {
            return _integrationEventHandlers.TryGetValue(integrationEventTypeName, out var subscriptions)
                ? subscriptions
                : new HashSet<IntegrationEventSubscription>();
        }

        public IReadOnlyCollection<IntegrationEventSubscription> FetchSubscriptions<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent
        {
            return this.FetchSubscriptions(this.GetIntegrationEventTypeName<TIntegrationEvent>());
        }

        public Type? TryGetIntegrationEventType(string integrationEventTypeName)
        {
            return _integrationEventTypes.SingleOrDefault(integrationEventType => integrationEventType.Name == integrationEventTypeName);
        }

        public void AddSubscription<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            this.AddSubscription(this.GetIntegrationEventTypeName<TIntegrationEvent>(), new IntegrationEventSubscription(typeof(TIntegrationEventHandler)));

            _integrationEventTypes.Add(typeof(TIntegrationEvent));
        }

        public void AddSubscription<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            this.AddSubscription(integrationEventTypeName, new DynamicIntegrationEventSubscription(typeof(TDynamicIntegrationEventHandler)));
        }

        public void RemoveSubscription<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            if (this.TryGetSubscription<TIntegrationEvent, TIntegrationEventHandler>(out var subscription))
            {
                this.RemoveSubscription(this.GetIntegrationEventTypeName<TIntegrationEvent>(), subscription);
            }
        }

        public void RemoveSubscription<TDynamicIntegrationEventHandler>(string integrationEventTypeName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            if (this.TryGetSubscription<TDynamicIntegrationEventHandler>(integrationEventTypeName, out var subscription))
            {
                this.RemoveSubscription(integrationEventTypeName, subscription);
            }
        }

        public string GetIntegrationEventTypeName<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent
        {
            return typeof(TIntegrationEvent).Name;
        }

        private void AddSubscription(string integrationEventTypeName, IntegrationEventSubscription integrationEventSubscription)
        {
            if (!_integrationEventHandlers.ContainsKey(integrationEventTypeName))
            {
                _integrationEventHandlers.Add(
                    integrationEventTypeName,
                    new HashSet<IntegrationEventSubscription>
                    {
                        integrationEventSubscription
                    }
                );
            }
            else
            {
                if (!_integrationEventHandlers[integrationEventTypeName].Add(integrationEventSubscription))
                {
                    throw new InvalidOperationException($"The {integrationEventSubscription} has already subscribed to {integrationEventTypeName}.");
                }
            }
        }

        private void RemoveSubscription(string integrationEventTypeName, IntegrationEventSubscription integrationEventSubscription)
        {
            _integrationEventHandlers[integrationEventTypeName].Remove(integrationEventSubscription);

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
