// Filename: UserAddressResponse.cs
// Date Created: 2019-11-07
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System;

using Newtonsoft.Json;

namespace eDoxa.Identity.Responses
{
    [JsonObject]
    public class UserAddressResponse
    {
        [JsonProperty("id")]
        public Guid Id { get; set; }

        [JsonProperty("type", Required = Required.AllowNull)]
        public string Type { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("line1")]
        public string Line1 { get; set; }

        [JsonProperty("line2", Required = Required.AllowNull)]
        public string Line2 { get; set; }

        [JsonProperty("city")]
        public string City { get; set; }

        [JsonProperty("state", Required = Required.AllowNull)]
        public string State { get; set; }

        [JsonProperty("postalCode", Required = Required.AllowNull)]
        public string PostalCode { get; set; }
    }
}
