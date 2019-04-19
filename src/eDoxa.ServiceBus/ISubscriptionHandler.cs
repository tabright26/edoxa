// Filename: ISubscriptionHandler.cs
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

using JetBrains.Annotations;

namespace eDoxa.ServiceBus
{
    /// <summary>
    ///     Represents the subscription handler.
    /// </summary>
    /// <remarks>
    ///     This subscription handler allows to register multiple integration event handlers (subscribers) to an
    ///     <see cref="IntegrationEvent" />.
    /// </remarks>
    public interface ISubscriptionHandler
    {
        /// <summary>
        ///     Indicating whether the <see cref="ISubscriptionHandler" /> is empty.
        /// </summary>
        bool IsEmpty { get; }

        /// <summary>
        ///     Adds a <see cref="Subscription" /> to the specified <see cref="IntegrationEvent" /> key aggregator.
        /// </summary>
        /// <typeparam name="TIntegrationEvent">The type of the <see cref="IntegrationEvent" />.</typeparam>
        /// <typeparam name="TDynamicIntegrationEventHandler">
        ///     The type of the <see cref="IIntegrationEventHandler{TIntegrationEvent}" />.
        /// </typeparam>
        void AddSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        /// <summary>
        ///     Adds a dynamic <see cref="Subscription" /> to the specified <see cref="IntegrationEvent" /> key aggregator.
        /// </summary>
        /// <typeparam name="TDynamicIntegrationEventHandler">The type of the <see cref="IDynamicIntegrationEventHandler" />.</typeparam>
        /// <param name="integrationEventKey">The <see cref="IntegrationEvent" /> key.</param>
        void AddDynamicSubscription<TDynamicIntegrationEventHandler>(string integrationEventKey)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;

        /// <summary>
        ///     Removes all <see cref="Subscription" /> from the <see cref="ISubscriptionHandler" />.
        /// </summary>
        void ClearSubscriptions();

        /// <summary>
        ///     Determines whether the <see cref="ISubscriptionHandler" /> contains a specific <see cref="IntegrationEvent" />.
        /// </summary>
        /// <param name="integrationEventKey">The <see cref="IntegrationEvent" /> key.</param>
        /// <returns>
        ///     True if the <see cref="ISubscriptionHandler" /> contains an <see cref="IntegrationEvent" /> with the specified key;
        ///     otherwise,
        ///     false.
        /// </returns>
        bool ContainsIntegrationEvent(string integrationEventKey);

        /// <summary>
        ///     Determines whether the <see cref="ISubscriptionHandler" /> contains a specific <see cref="IntegrationEvent" />.
        /// </summary>
        /// <typeparam name="TIntegrationEvent">The type of the <see cref="IntegrationEvent" />.</typeparam>
        /// <returns>
        ///     True if the <see cref="ISubscriptionHandler" /> contains an <see cref="IntegrationEvent" /> with the specified key;
        ///     otherwise,
        ///     false.
        /// </returns>
        bool ContainsIntegrationEvent<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent;

        /// <summary>
        ///     Searches for a <see cref="Subscription" /> that matches the specified <see cref="IntegrationEvent" /> key and
        ///     <see cref="IIntegrationEventHandler{TIntegrationEvent}" /> through all the <see cref="IntegrationEvent" />
        ///     subscriptions.
        /// </summary>
        /// <param name="integrationEventKey">The <see cref="IntegrationEvent" /> key.</param>
        /// <param name="integrationEventHandlerType">Type of the <see cref="IIntegrationEventHandler{TIntegrationEvent}" />.</param>
        /// <returns>The <see cref="Subscription" /> that matches an instance of the <see cref="IntegrationEvent" /> subscriptions.</returns>
        [CanBeNull]
        Subscription FindSubscription(string integrationEventKey, Type integrationEventHandlerType);

        /// <summary>
        ///     Searches for a <see cref="Subscription" /> that matches the specified <see cref="IntegrationEvent" /> key and
        ///     <see cref="IIntegrationEventHandler{TIntegrationEvent}" /> through all the <see cref="IntegrationEvent" />
        ///     subscriptions.
        /// </summary>
        /// <typeparam name="TIntegrationEvent">The type of the <see cref="IntegrationEvent" />.</typeparam>
        /// <typeparam name="TDynamicIntegrationEventHandler">The type of the <see cref="IDynamicIntegrationEventHandler" />.</typeparam>
        /// <returns>The <see cref="Subscription" /> that matches an instance of the <see cref="IntegrationEvent" /> subscriptions.</returns>
        [CanBeNull]
        Subscription FindSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        /// <summary>
        ///     Searches for a dynamic <see cref="Subscription" /> that matches the specified <see cref="IntegrationEvent" /> key
        ///     and <see cref="IDynamicIntegrationEventHandler" /> through all the <see cref="IntegrationEvent" /> dynamic
        ///     subscriptions.
        /// </summary>
        /// <typeparam name="TDynamicIntegrationEventHandler">The type of the <see cref="IDynamicIntegrationEventHandler" />.</typeparam>
        /// <param name="integrationEventKey">The <see cref="IntegrationEvent" /> key.</param>
        /// <returns>
        ///     The dynamic <see cref="Subscription" /> that matches an instance of the <see cref="IntegrationEvent" />
        ///     dynamic subscriptions.
        /// </returns>
        [CanBeNull]
        Subscription FindDynamicSubscription<TDynamicIntegrationEventHandler>(string integrationEventKey)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;

        /// <summary>
        ///     Retrieves all the subscriptions that match the specified <see cref="IntegrationEvent" /> key.
        /// </summary>
        /// <param name="integrationEventKey">The <see cref="IntegrationEvent" /> key.</param>
        /// <returns>All subscriptions associated with an <see cref="IntegrationEvent" /> as <see cref="ICollection{T}" />.</returns>
        ICollection<Subscription> FindAllSubscriptions(string integrationEventKey);

        /// <summary>
        ///     Retrieves all the subscriptions that match the specified <see cref="IntegrationEvent" /> type.
        /// </summary>
        /// <typeparam name="TIntegrationEvent">The type of the <see cref="IntegrationEvent" />.</typeparam>
        /// <returns>All subscriptions associated with an <see cref="IntegrationEvent" /> as <see cref="ICollection{T}" />.</returns>
        ICollection<Subscription> FindAllSubscriptions<TIntegrationEvent>()
        where TIntegrationEvent : IntegrationEvent;

        /// <summary>
        ///     Gets the <see cref="IntegrationEvent" /> key.
        /// </summary>
        /// <typeparam name="TIntegrationEvent">The type of the <see cref="IntegrationEvent" />.</typeparam>
        /// <returns>The <see cref="IntegrationEvent" /> key.</returns>
        string GetIntegrationEventKey<TIntegrationEvent>();

        /// <summary>
        ///     Gets the <see cref="IntegrationEvent" /> type.
        /// </summary>
        /// <param name="integrationEventKey">The <see cref="IntegrationEvent" /> key.</param>
        /// <returns>The <see cref="IntegrationEvent" /> type.</returns>
        [CanBeNull]
        Type GetIntegrationEventType(string integrationEventKey);

        /// <summary>
        ///     Removes a <see cref="Subscription" /> from the specified <see cref="IntegrationEvent" /> key aggregator.
        /// </summary>
        /// <typeparam name="TIntegrationEvent">The type of the <see cref="IntegrationEvent" />.</typeparam>
        /// <typeparam name="TDynamicIntegrationEventHandler">
        ///     The type of the
        ///     <see cref="IIntegrationEventHandler{TIntegrationEvent}" />.
        /// </typeparam>
        void RemoveSubscription<TIntegrationEvent, TDynamicIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TDynamicIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        /// <summary>
        ///     Removes a dynamic <see cref="Subscription" /> from the specified <see cref="IntegrationEvent" /> key aggregator.
        /// </summary>
        /// <typeparam name="TDynamicIntegrationEventHandler">The type of the <see cref="IDynamicIntegrationEventHandler" />.</typeparam>
        /// <param name="integrationEventKey">The <see cref="IntegrationEvent" /> key.</param>
        void RemoveDynamicSubscription<TDynamicIntegrationEventHandler>(string integrationEventKey)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;

        /// <summary>
        ///     Occurs when an <see cref="IntegrationEvent" /> removed.
        /// </summary>
        event EventHandler<string> OnIntegrationEventRemoved;
    }
}