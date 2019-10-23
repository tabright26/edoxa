// Filename: BankAccountResponse.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.Areas.Stripe.Responses
{
    [JsonObject]
    public sealed class BankAccountResponse
    {
        [JsonProperty("bankName")]
        public string BankName { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("currency")]
        public string Currency { get; set; }

        [JsonProperty("last4")]
        public string Last4 { get; set; }

        [JsonProperty("status")]
        public string Status { get; set; }

        [JsonProperty("defaultForCurrency")]
        public bool DefaultForCurrency { get; set; }
    }
}
