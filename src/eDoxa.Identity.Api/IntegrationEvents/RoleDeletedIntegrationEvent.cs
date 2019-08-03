// Filename: RoleDeletedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.Constants;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    internal sealed class RoleDeletedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public RoleDeletedIntegrationEvent(string roleName)
        {
            RoleName = roleName;
        }

        [JsonProperty]
        public string RoleName { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.RoleDeleted;
    }
}
