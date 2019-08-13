// Filename: AddressResponse.cs
// Date Created: 2019-08-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using eDoxa.Identity.Api.Infrastructure.Models;

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.Responses
{
    [JsonObject]
    public class AddressResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type")]
        public UserAddressType Type { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("line1")]
        public string Line1 { get; set; }

        [JsonProperty("line2")]
        public string Line2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("postalCode")]
        public string PostalCode { get; set; }
    }
}
