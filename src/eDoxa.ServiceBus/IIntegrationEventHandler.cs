// Filename: IIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

namespace eDoxa.ServiceBus
{
    /// <summary>
    ///     The <see cref="IntegrationEvent" /> handler.
    /// </summary>
    public interface IIntegrationEventHandler
    {
        // Marker interface
    }

    /// <typeparam name="TIntegrationEvent">Type of an <see cref="IntegrationEvent" />.</typeparam>
    public interface IIntegrationEventHandler<in TIntegrationEvent> : IIntegrationEventHandler
    where TIntegrationEvent : IntegrationEvent
    {
        /// <summary>
        ///     Handle the <see cref="IntegrationEvent" />.
        /// </summary>
        /// <param name="integrationEvent">The <see cref="IntegrationEvent" />.</param>
        /// <returns>
        ///     A <see cref="Task" /> that completes when handle the <see cref="IntegrationEvent" /> has completed processing.
        /// </returns>
        Task Handle(TIntegrationEvent integrationEvent);
    }
}