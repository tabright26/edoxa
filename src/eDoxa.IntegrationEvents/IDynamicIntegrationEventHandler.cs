// Filename: IDynamicIntegrationEventHandler.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

namespace eDoxa.IntegrationEvents
{
    /// <summary>
    ///     The dynamic integration event handler.
    /// </summary>
    public interface IDynamicIntegrationEventHandler
    {
        /// <summary>
        ///     Handle the dynamic integration event.
        /// </summary>
        /// <param name="integrationEvent">The dynamic integration event.</param>
        /// <returns>
        ///     A <see cref="Task" /> that completes when handle the dynamic integration event has completed processing.
        /// </returns>
        Task Handle(dynamic integrationEvent);
    }
}