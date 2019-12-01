// Filename: UserCreatedIntegrationEvent.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserCreatedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserCreatedIntegrationEvent(UserId userId, string email, Country country)
        {
            UserId = userId;
            Email = email;
            Country = country;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public string Email { get; }

        [JsonProperty]
        public Country Country { get; }

        [JsonIgnore]
        public string Name => Seedwork.Application.Constants.IntegrationEvents.UserCreated;
    }
}
