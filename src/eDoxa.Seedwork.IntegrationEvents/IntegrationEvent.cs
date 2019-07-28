// Filename: IntegrationEvent.cs
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