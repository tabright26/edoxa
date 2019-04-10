// Filename: IEventBusService.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.ServiceBus
{
    /// <summary>
    ///     <para>
    ///         The event bus service is to subscribe the microservices to the events they want to receive. That should be done
    ///         in the receiver microservices. Each receiver microservice needs to implement when starting the service so it
    ///         subscribes to the events it needs.
    ///     </para>
    ///     <para>
    ///         A microservice need to publish an event through the event bus channel and intercepted by the
    ///         receiver subscriber. The subscriber microservice will be listening through event bus channel and handle the
    ///         <see cref="IntegrationEvent" />.
    ///     </para>
    /// </summary>
    /// <remarks>
    ///     When subscribing to the <see cref="IntegrationEvent" />, that makes the microservice aware of any changes published
    ///     by another
    ///     microservice.
    /// </remarks>
    public interface IEventBusService : IDisposable
    {
        /// <summary>
        ///     Publishing <see cref="IntegrationEvent" /> through the event bus channel.
        /// </summary>
        /// <remarks>
        ///     This method definition is the same for all event buses (RabbitMQ, AzureServiceBus, etc.).
        /// </remarks>
        /// <param name="integrationEvent">The <see cref="T:eDoxa.ServiceBus.IntegrationEvent" />.</param>
        void Publish(IntegrationEvent integrationEvent);

        /// <summary>
        ///     Subscribe to an <see cref="IntegrationEvent" />.
        /// </summary>
        /// <typeparam name="TIntegrationEvent">The type of <see cref="IntegrationEvent" />.</typeparam>
        /// <typeparam name="TIntegrationEventHandler">The type of <see cref="IntegrationEvent" />.</typeparam>
        void Subscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        /// <summary>
        ///     Subscribe dynamically to an <see cref="IntegrationEvent" />.
        /// </summary>
        /// <typeparam name="TDynamicIntegrationEventHandler">
        ///     The type of dynamic <see cref="IntegrationEvent" />.
        /// </typeparam>
        /// <param name="integrationEventName">The <see cref="IntegrationEvent" /> name.</param>
        void SubscribeDynamic<TDynamicIntegrationEventHandler>(string integrationEventName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;

        /// <summary>
        ///     Unsubscribe of an <see cref="IntegrationEvent" />.
        /// </summary>
        /// <typeparam name="TIntegrationEvent">The type of <see cref="IntegrationEvent" />.</typeparam>
        /// <typeparam name="TIntegrationEventHandler">The type of <see cref="IntegrationEvent" />.</typeparam>
        void Unsubscribe<TIntegrationEvent, TIntegrationEventHandler>()
        where TIntegrationEvent : IntegrationEvent
        where TIntegrationEventHandler : IIntegrationEventHandler<TIntegrationEvent>;

        /// <summary>
        ///     Unsubscribe dynamically of an <see cref="IntegrationEvent" />.
        /// </summary>
        /// <typeparam name="TDynamicIntegrationEventHandler">
        ///     The type of dynamic <see cref="IntegrationEvent" />.
        /// </typeparam>
        /// <param name="integrationEventName">The <see cref="IntegrationEvent" /> name.</param>
        void UnsubscribeDynamic<TDynamicIntegrationEventHandler>(string integrationEventName)
        where TDynamicIntegrationEventHandler : IDynamicIntegrationEventHandler;
    }
}