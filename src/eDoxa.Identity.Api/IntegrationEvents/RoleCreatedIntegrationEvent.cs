// Filename: RoleCreatedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class RoleCreatedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public RoleCreatedIntegrationEvent(string roleName)
        {
            RoleName = roleName;
        }

        [JsonProperty]
        public string RoleName { get; }

        [JsonIgnore]
        public string Name => Seedwork.Application.Constants.IntegrationEvents.RoleCreated;
    }
}
