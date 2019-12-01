// Filename: UpdateAddressRequest.cs
// Date Created: 2019-11-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Identity.Requests
{
    [JsonObject]
    public sealed class UpdateAddressRequest
    {
        [JsonConstructor]
        public UpdateAddressRequest(
            string line1,
            string line2,
            string city,
            string state,
            string postalCode
        )
        {
            Line1 = line1;
            Line2 = line2;
            City = city;
            State = state;
            PostalCode = postalCode;
        }

        public UpdateAddressRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("line1")]
        public string Line1 { get; private set; }

        [JsonProperty("line2", Required = Required.AllowNull)]
        public string Line2 { get; private set; }

        [JsonProperty("city")]
        public string City { get; private set; }

        [JsonProperty("state", Required = Required.AllowNull)]
        public string State { get; private set; }

        [JsonProperty("postalCode", Required = Required.AllowNull)]
        public string PostalCode { get; private set; }
    }
}
