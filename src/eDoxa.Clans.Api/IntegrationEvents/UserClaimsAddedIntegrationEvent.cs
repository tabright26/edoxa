// Filename: UserClaimsAddedIntegrationEvent.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Clans.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserClaimsAddedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserClaimsAddedIntegrationEvent(UserId userId, Claims claims)
        {
            UserId = userId;
            Claims = claims;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public Claims Claims { get; }

        [JsonIgnore]
        public string Name => Seedwork.Application.Constants.IntegrationEvents.UserClaimsAdded;
    }
}
