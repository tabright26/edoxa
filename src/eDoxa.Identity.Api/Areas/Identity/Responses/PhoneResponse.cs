// Filename: PhoneResponse.cs
// Date Created: 2019-10-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Identity.Api.Areas.Identity.Responses
{
    [JsonObject]
    public sealed class PhoneResponse
    {
        [JsonProperty("phoneNumber")]
        public string PhoneNumber { get; set; }

        [JsonProperty("phoneNumberVerified")]
        public bool PhoneNumberVerified { get; set; }
    }
}
