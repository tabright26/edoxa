﻿// Filename: UserEmailChangedIntegrationEvent.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserEmailChangedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserEmailChangedIntegrationEvent(UserId userId, string email)
        {
            UserId = userId;
            Email = email;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public string Email { get; }

        [JsonIgnore]
        public string Name => Seedwork.Application.Constants.IntegrationEvents.UserEmailChanged;
    }
}
