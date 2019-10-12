﻿// Filename: UserCreatedIntegrationEvent.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserCreatedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserCreatedIntegrationEvent(UserId userId, string email, string country)
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
        public string Country { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserCreated;
    }
}
