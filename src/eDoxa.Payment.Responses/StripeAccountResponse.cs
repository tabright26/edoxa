// Filename: StripeAccountResponse.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Payment.Responses
{
    [JsonObject]
    public sealed class StripeAccountResponse
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}
