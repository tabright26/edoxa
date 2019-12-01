// Filename: RoleClaimsRemovedIntegrationEvent.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class RoleClaimsRemovedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public RoleClaimsRemovedIntegrationEvent(string roleName, Claims claims)
        {
            RoleName = roleName;
            Claims = claims;
        }

        [JsonProperty]
        public string RoleName { get; }

        [JsonProperty]
        public Claims Claims { get; }

        [JsonIgnore]
        public string Name => Seedwork.Application.Constants.IntegrationEvents.RoleClaimRemoved;
    }
}
