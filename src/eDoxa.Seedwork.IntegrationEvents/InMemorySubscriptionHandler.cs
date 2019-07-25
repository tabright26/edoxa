// Filename: InMemorySubscriptionHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;

using eDoxa.Seedwork.IntegrationEvents.Exceptions;

using JetBrains.Annotations;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public sealed class InMemorySubscriptionHandler : ISubscriptionHandler
    {
        private readonly HashSet<Type> _integrationEventTypes;
        private readonly Dictionary<string, Collection<Subscription>> _integrationEventHandlers;

        /// <summary>
        ///     Initializes a new instance of the <see cref="InMemorySubscriptionHandler" /> class.
        /// </summary>
        public InMemorySubscriptionHandler()
        {
            _integrationEventTypes = new HashSet<Type>();
            _integrationEventHandlers = new Dictionary<string, Collection<Subscription>>();
        }

        public event EventHandler<string> OnIntegrationEventRemoved;

        public bool IsEmpty
        {
            get
            {
                return !_integrationEventHandlers.Keys.Any();
            }
        }

        public void AddSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var integrationEventKey = this.GetIntegrationEventKey<TIntegrationEvent>();

            this.AddSubscription(integrationEventKey, typeof(TDynamicIntegrationEventHandler), false);

            _integrationEventTypes.Add(typeof(TIntegrationEvent));
        }

        public void AddDynamicSubscription<TDynamicIntegrationEventHandler>(string integrationEventKey)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            this.AddSubscription(integrationEventKey, typeof(TDynamicIntegrationEventHandler), true);
        }

        public void ClearSubscriptions()
        {
            _integrationEventHandlers.Clear();
        }

        public bool ContainsIntegrationEvent(string integrationEventKey)
        {
            return _integrationEventHandlers.ContainsKey(integrationEventKey);
        }

        public bool ContainsIntegrationEvent<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent
        {
            var integrationEventKey = this.GetIntegrationEventKey<TIntegrationEvent>();

            return this.ContainsIntegrationEvent(integrationEventKey);
        }

        public Subscription FindSubscription(string integrationEventKey, Type integrationEventHandlerType)
        {
            return !this.ContainsIntegrationEvent(integrationEventKey) ?
                null :
                _integrationEventHandlers[integrationEventKey]
                    .SingleOrDefault(subscription => subscription.IntegrationEventHandlerType == integrationEventHandlerType);
        }

        public Subscription FindSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var integrationEventKey = this.GetIntegrationEventKey<TIntegrationEvent>();

            return this.FindSubscription(integrationEventKey, typeof(TDynamicIntegrationEventHandler));
        }

        public Subscription FindDynamicSubscription<TDynamicIntegrationEventHandler>(string integrationEventKey)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            return this.FindSubscription(integrationEventKey, typeof(TDynamicIntegrationEventHandler));
        }

        public ICollection<Subscription> FindAllSubscriptions(string integrationEventKey)
        {
            return _integrationEventHandlers[integrationEventKey];
        }

        public ICollection<Subscription> FindAllSubscriptions<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent
        {
            var integrationEventKey = this.GetIntegrationEventKey<TIntegrationEvent>();

            return this.FindAllSubscriptions(integrationEventKey);
        }

        public string GetIntegrationEventKey<TIntegrationEvent>()
        {
            return typeof(TIntegrationEvent).Name;
        }

        public Type GetIntegrationEventType(string integrationEventKey)
        {
            return _integrationEventTypes.SingleOrDefault(type => type.Name == integrationEventKey);
        }

        public void RemoveSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>
        {
            var subscription = this.FindSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>();

            var integrationEventKey = this.GetIntegrationEventKey<TIntegrationEvent>();

            this.RemoveSubscription(integrationEventKey, subscription);
        }

        public void RemoveDynamicSubscription<TDynamicIntegrationEventHandler>(string integrationEventKey)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler
        {
            var subscription = this.FindDynamicSubscription<TDynamicIntegrationEventHandler>(integrationEventKey);

            this.RemoveSubscription(integrationEventKey, subscription);
        }

        /// <summary>
        ///     Adds the specified <see cref="IntegrationEvent" /> key and a <see cref="Subscription" /> to the dictionary.
        /// </summary>
        /// <param name="integrationEventKey">The <see cref="IntegrationEvent" /> key.</param>
        /// <param name="integrationEventHandlerType">Type of the integration event handler.</param>
        /// <param name="isDynamic">if set to <c>true</c> [is dynamic].</param>
        private void AddSubscription(string integrationEventKey, Type integrationEventHandlerType, bool isDynamic)
        {
            if (!this.ContainsIntegrationEvent(integrationEventKey))
            {
                _integrationEventHandlers.Add(integrationEventKey, new Collection<Subscription>());
            }

            if (_integrationEventHandlers[integrationEventKey].Any(subscription => subscription.IntegrationEventHandlerType == integrationEventHandlerType))
            {
                throw new SubscriptionException(
                    nameof(AddSubscription),
                    new ArgumentException(
                        $"Handler Type {integrationEventHandlerType.Name} already registered for '{integrationEventKey}'",
                        nameof(integrationEventHandlerType)
                    )
                );
            }

            _integrationEventHandlers[integrationEventKey]
                .Add(
                    isDynamic ?
                        Subscription.FromDynamicIntegrationEventHandler(integrationEventHandlerType) :
                        Subscription.FromIntegrationEventHandler(integrationEventHandlerType)
                );
        }

        /// <summary>
        ///     Removes the specified <see cref="IntegrationEvent" /> key and a <see cref="Subscription" /> to the dictionary.
        /// </summary>
        /// <param name="integrationEventKey">The <see cref="IntegrationEvent" /> key.</param>
        /// <param name="subscription">The <see cref="Subscription" />.</param>
        private void RemoveSubscription(string integrationEventKey, [CanBeNull] Subscription subscription)
        {
            if (subscription == null)
            {
                return;
            }

            _integrationEventHandlers[integrationEventKey].Remove(subscription);

            if (_integrationEventHandlers[integrationEventKey].Any())
            {
                return;
            }

            _integrationEventHandlers.Remove(integrationEventKey);

            var integrationEventType = _integrationEventTypes.SingleOrDefault(integrationEvent => integrationEvent.Name == integrationEventKey);

            if (integrationEventType != null)
            {
                _integrationEventTypes.Remove(integrationEventType);
            }

            OnIntegrationEventRemoved?.Invoke(this, integrationEventKey);
        }
    }
}