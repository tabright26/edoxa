// Filename: IntegrationEventModel.cs
// Date Created: 2019-07-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.ServiceBus.Infrastructure
{
    public sealed class IntegrationEventModel
    {
        public IntegrationEventModel(IntegrationEvent integrationEvent) : this()
        {
            Id = integrationEvent.Id;
            Timestamp = integrationEvent.Timestamp;
            TypeName = integrationEvent.GetType().FullName;
            Content = JsonConvert.SerializeObject(integrationEvent);
        }

        private IntegrationEventModel()
        {
            Status = IntegrationEventStatus.NotPublished;
            PublishAttempted = 0;
        }

        public Guid Id { get; private set; }

        public DateTime Timestamp { get; private set; }

        public string TypeName { get; private set; }

        public string Content { get; private set; }

        public IntegrationEventStatus Status { get; private set; }

        public int PublishAttempted { get; private set; }

        public void MarkAsPublished()
        {
            Status = IntegrationEventStatus.Published;

            PublishAttempted++;
        }
    }
}
