// Filename: UserClaimsReplacedIntegrationEvent.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserClaimsReplacedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserClaimsReplacedIntegrationEvent(
            UserId userId,
            int claimCount,
            IDictionary<string, string> claims,
            IDictionary<string, string> newClaims
        )
        {
            UserId = userId;
            ClaimCount = claimCount;
            Claims = claims;
            NewClaims = newClaims;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public int ClaimCount { get; }

        [JsonProperty]
        public IDictionary<string, string> Claims { get; }

        [JsonProperty]
        public IDictionary<string, string> NewClaims { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserClaimsReplaced;
    }
}
