// Filename: IIntegrationEventLogRepository.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Data.Common;
using System.Threading.Tasks;

namespace eDoxa.ServiceBus
{
    /// <summary>
    ///     The <see cref="IntegrationEvent" /> logger repository. Used to change the state of the
    ///     <see cref="IntegrationEventLogEntry" /> record for each
    ///     <see cref="IntegrationEvent" /> published.
    /// </summary>
    /// <remarks>
    ///     This is an implementation of the repository pattern implementation.
    /// </remarks>
    public interface IIntegrationEventLogRepository
    {
        /// <summary>
        ///     Save the integration events in the SQL database.
        /// </summary>
        /// <param name="integrationEvent">The <see cref="IntegrationEvent" />.</param>
        /// <param name="transaction">The <see cref="DbTransaction" />.</param>
        /// <returns>
        ///     A <see cref="Task" /> that completes when saving the integration events has
        ///     completed processing.
        /// </returns>
        Task SaveIntegrationEventAsync(IntegrationEvent integrationEvent, DbTransaction transaction);

        /// <summary>
        ///     Mark an <see cref="IntegrationEvent" /> as published.
        /// </summary>
        /// <param name="integrationEvent">The <see cref="IntegrationEvent" />.</param>
        /// <returns>
        ///     A <see cref="Task" /> that completes when the <see cref="IntegrationEvent" /> is marked as published has
        ///     completed processing.
        /// </returns>
        Task MarkIntegrationEventAsPublishedAsync(IntegrationEvent integrationEvent);
    }
}