﻿// Filename: StripeBankAccountResponse.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Payment.Responses
{
    [JsonObject]
    public sealed class StripeBankAccountResponse
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
