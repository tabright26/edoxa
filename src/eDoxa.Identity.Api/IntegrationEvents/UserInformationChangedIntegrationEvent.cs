// Filename: UserInformationChangedIntegrationEvent.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Misc;
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
            Dob dob
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
        public Dob Dob { get; }

        [JsonIgnore]
        public string Name => Seedwork.Application.Constants.IntegrationEvents.UserInformationChanged;
    }
}
