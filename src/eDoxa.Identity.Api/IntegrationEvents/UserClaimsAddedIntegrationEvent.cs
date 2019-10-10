// Filename: UserClaimsAddedIntegrationEvent.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserClaimsAddedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserClaimsAddedIntegrationEvent(UserId userId, IDictionary<string, string> claims)
        {
            UserId = userId;
            Claims = claims;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public IDictionary<string, string> Claims { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserClaimsAdded;
    }
}
