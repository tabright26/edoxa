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

namespace eDoxa.Seedwork.IntegrationEvents.Infrastructure
{
    public class IntegrationEventLogEntry
    {
        public IntegrationEventLogEntry(IntegrationEvent integrationEvent) : this()
        {
            Id = integrationEvent.Id;
            Created = integrationEvent.Timestamp;
            TypeFullName = integrationEvent.GetType().FullName;
            JsonObject = JsonConvert.SerializeObject(integrationEvent);
        }

        private IntegrationEventLogEntry()
        {
            State = IntegrationEventState.NotPublished;
            PublishAttempted = 0;
        }

        public Guid Id { get; private set; }

        public DateTime Created { get; private set; }

        public string TypeFullName { get; private set; }

        public string JsonObject { get; private set; }

        public IntegrationEventState State { get; private set; }

        public int PublishAttempted { get; private set; }

        public void MarkAsPublished()
        {
            State = IntegrationEventState.Published;

            PublishAttempted++;
        }
    }
}