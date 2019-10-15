// Filename: UserInformationChangedIntegrationEvent.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.ServiceBus.Abstractions;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.IntegrationEvents
{
    [JsonObject]
    public sealed class UserInformationChangedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserInformationChangedIntegrationEvent(
            UserId userId,
            string firstName,
            string lastName,
            Gender gender,
            DateTime dob
        )
        {
            UserId = userId;
            FirstName = firstName;
            LastName = lastName;
            Gender = gender;
            Dob = dob;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public string FirstName { get; }

        [JsonProperty]
        public string LastName { get; }

        [JsonProperty]
        public Gender Gender { get; }

        [JsonProperty]
        public DateTime Dob { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserInformationChanged;
    }
}
