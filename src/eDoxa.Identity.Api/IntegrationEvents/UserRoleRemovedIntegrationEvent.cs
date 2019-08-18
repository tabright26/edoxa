// Filename: UserRoleRemovedIntegrationEvent.cs
// Date Created: 2019-06-25
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
    internal sealed class UserRoleRemovedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserRoleRemovedIntegrationEvent(Guid userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }

        [JsonProperty]
        public Guid UserId { get; }

        [JsonProperty]
        public string RoleName { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserRoleRemoved;
    }
}
