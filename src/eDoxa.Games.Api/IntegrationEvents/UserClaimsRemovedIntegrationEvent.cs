// Filename: UserClaimsRemovedIntegrationEvent.cs
// Date Created: 2019-10-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Seedwork.Security;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Games.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserClaimsRemovedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserClaimsRemovedIntegrationEvent(UserId userId, Claims claims)
        {
            UserId = userId;
            Claims = claims;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public Claims Claims { get; }

        [JsonIgnore]
        public string Name => Seedwork.Application.Constants.IntegrationEvents.UserClaimsRemoved;
    }
}
