// Filename: UserAddressChangedIntegrationEvent.cs
// Date Created: 2019-10-10
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
    public sealed class UserAddressChangedIntegrationEvent : IIntegrationEvent
    {
        [JsonConstructor]
        public UserAddressChangedIntegrationEvent(
            UserId userId,
            string line1,
            string? line2,
            string? state,
            string city,
            string postalCode
        )
        {
            UserId = userId;
            Line1 = line1;
            Line2 = line2;
            State = state;
            City = city;
            PostalCode = postalCode;
        }

        [JsonProperty]
        public UserId UserId { get; }

        [JsonProperty]
        public string Line1 { get; }

        [JsonProperty]
        public string? Line2 { get; }

        [JsonProperty]
        public string? State { get; }

        [JsonProperty]
        public string City { get; }

        [JsonProperty]
        public string PostalCode { get; }

        [JsonIgnore]
        public string Name => IntegrationEventNames.UserAddressChanged;
    }
}
