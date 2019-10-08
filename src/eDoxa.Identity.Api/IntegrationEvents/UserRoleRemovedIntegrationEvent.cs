// Filename: UserRoleRemovedIntegrationEvent.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserRoleRemovedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserRoleRemovedIntegrationEvent(UserId userId, string roleName)
        {
            UserId = userId;
            RoleName = roleName;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public string RoleName { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserRoleRemoved;
    }
}
