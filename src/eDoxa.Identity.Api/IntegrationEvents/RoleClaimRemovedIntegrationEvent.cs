// Filename: RoleClaimRemovedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    internal sealed class RoleClaimRemovedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public RoleClaimRemovedIntegrationEvent(string roleName, string claimType, string claimValue)
        {
            RoleName = roleName;
            ClaimType = claimType;
            ClaimValue = claimValue;
        }

        [JsonProperty]
        public string RoleName { get; }

        [JsonProperty]
        public string ClaimType { get; }

        [JsonProperty]
        public string ClaimValue { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.RoleClaimRemoved;
    }
}
