// Filename: UserClaimsReplacedIntegrationEvent.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;
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
            Claims claims,
            Claims newClaims
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
        public Claims Claims { get; }

        [JsonProperty]
        public Claims NewClaims { get; }

        [JsonIgnore]
        public string Name => Seedwork.Application.Constants.IntegrationEvents.UserClaimsReplaced;
    }
}
