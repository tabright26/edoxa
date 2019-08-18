// Filename: UserCreatedIntegrationEvent.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    internal sealed class UserCreatedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserCreatedIntegrationEvent(Guid userId)
        {
            UserId = userId;
        }

        [JsonProperty]
        public Guid UserId { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserCreated;
    }
}
