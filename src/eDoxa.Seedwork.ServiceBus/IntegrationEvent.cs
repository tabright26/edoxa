// Filename: IntegrationEvent.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Newtonsoft.Json;

namespace eDoxa.Seedwork.ServiceBus
{
    [JsonObject]
    public abstract class IntegrationEvent
    {
        protected IntegrationEvent()
        {
            Id = Guid.NewGuid();
            Timestamp = DateTime.UtcNow;
        }

        [JsonProperty]
        public Guid Id { get; set; }

        [JsonProperty]
        public DateTime Timestamp { get; set; }
    }
}
