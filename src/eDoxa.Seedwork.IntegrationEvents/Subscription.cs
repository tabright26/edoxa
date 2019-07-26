// Filename: Subscription.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Seedwork.IntegrationEvents
{
    /// <summary>
    ///     Represents an event bus subscription.
    /// </summary>
    public sealed class Subscription
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="Subscription" /> class.
        /// </summary>
        /// <param name="integrationEventHandlerType">
        ///     The <see cref="IIntegrationEventHandler{TIntegrationEvent}" /> or
        ///     <see cref="IDynamicIntegrationEventHandler" /> type.
        /// </param>
        /// <param name="isDynamic">The [dynamic] or [typed] integration event handler.</param>
        private Subscription(Type integrationEventHandlerType, bool isDynamic)
        {
            IntegrationEventHandlerType = integrationEventHandlerType;
            IsDynamic = isDynamic;
        }

        /// <summary>
        ///     The [dynamic] or [typed] integration event handler.
        /// </summary>
        public bool IsDynamic { get; }

        /// <summary>
        ///     The <see cref="IIntegrationEventHandler{TIntegrationEvent}" /> or <see cref="IDynamicIntegrationEventHandler" />
        ///     type.
        /// </summary>
        public Type IntegrationEventHandlerType { get; }

        /// <summary>
        ///     Create a subscription from an <see cref="IIntegrationEventHandler{TIntegrationEvent}" />.
        /// </summary>
        /// <param name="integrationEventHandler">The type of the <see cref="IIntegrationEventHandler{TIntegrationEvent}" />.</param>
        /// <returns>The <see cref="Subscription" />.</returns>
        public static Subscription FromIntegrationEventHandler(Type integrationEventHandler)
        {
            return new Subscription(integrationEventHandler, false);
        }

        /// <summary>
        ///     Create a subscription from a <see cref="IDynamicIntegrationEventHandler" />.
        /// </summary>
        /// <param name="dynamicIntegrationEventHandler">The type of the <see cref="IDynamicIntegrationEventHandler" />.</param>
        /// <returns>The <see cref="Subscription" />.</returns>
        public static Subscription FromDynamicIntegrationEventHandler(Type dynamicIntegrationEventHandler)
        {
            return new Subscription(dynamicIntegrationEventHandler, true);
        }
    }
}