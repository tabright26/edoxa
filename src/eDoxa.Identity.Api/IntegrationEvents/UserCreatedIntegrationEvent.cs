// Filename: UserCreatedIntegrationEvent.cs
// Date Created: 2019-07-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserCreatedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserCreatedIntegrationEvent(UserId userId)
        {
            UserId = userId;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserCreated;
    }
}
