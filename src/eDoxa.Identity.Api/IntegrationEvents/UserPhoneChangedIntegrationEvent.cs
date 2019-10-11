// Filename: UserPhoneChangedIntegrationEvent.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Infrastructure.Models;
using eDoxa.Seedwork.Application;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserPhoneChangedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserPhoneChangedIntegrationEvent(UserId userId, string phoneNumber)
        {
            UserId = userId;
            PhoneNumber = phoneNumber;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public string PhoneNumber { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserPhoneChanged;
    }
}
