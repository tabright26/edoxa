// Filename: UserClaimsRemovedIntegrationEvent.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserClaimsRemovedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserClaimsRemovedIntegrationEvent(UserId userId, IDictionary<string, string> claims)
        {
            UserId = userId;
            Claims = claims;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public IDictionary<string, string> Claims { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserClaimsRemoved;
    }
}
