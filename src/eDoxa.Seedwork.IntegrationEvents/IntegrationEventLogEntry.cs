// Filename: IntegrationEventLogEntry.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.IntegrationEvents
{
    public class IntegrationEventLogEntry
    {
        public IntegrationEventLogEntry(IntegrationEvent integrationEvent) : this()
        {
            Id = integrationEvent.Id;
            Created = integrationEvent.Created;
            TypeFullName = integrationEvent.GetType().FullName;
            JsonObject = JsonConvert.SerializeObject(integrationEvent);
        }

        private IntegrationEventLogEntry()
        {
            State = IntegrationEventState.NotPublished;
            PublishAttempted = 0;
        }

        /// <summary>
        ///     The <see cref="IntegrationEvent" /> id.
        /// </summary>
        public Guid Id { get; private set; }

        /// <summary>
        ///     The <see cref="IntegrationEvent" /> created.
        /// </summary>
        public DateTime Created { get; private set; }

        /// <summary>
        ///     The <see cref="IntegrationEvent" /> type full name.
        /// </summary>
        public string TypeFullName { get; private set; }

        /// <summary>
        ///     The <see cref="IntegrationEvent" /> json object.
        /// </summary>
        public string JsonObject { get; private set; }

        /// <summary>
        ///     The <see cref="IntegrationEventLogEntry" /> state.
        /// </summary>
        public IntegrationEventState State { get; private set; }

        /// <summary>
        ///     The <see cref="IntegrationEventLogEntry" /> publish attempted.
        /// </summary>
        public int PublishAttempted { get; private set; }

        /// <summary>
        ///     Mark <see cref="IntegrationEventLogEntry" /> as published.
        /// </summary>
        public void MarkAsPublished()
        {
            State = IntegrationEventState.Published;

            PublishAttempted++;
        }
    }
}