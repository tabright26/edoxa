// Filename: AccountResponse.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.Areas.Stripe.Responses
{
    [JsonObject]
    public sealed class AccountResponse
    {
        [JsonProperty("enabled")]
        public bool Enabled { get; set; }
    }
}
