// Filename: PhoneResponse.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Identity.Responses
{
    [JsonObject]
    public sealed class PhoneResponse
    {
        [JsonProperty("number")]
        public string Number { get; set; }

        [JsonProperty("verified")]
        public bool Verified { get; set; }
    }
}
