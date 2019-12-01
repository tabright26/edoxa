// Filename: StripePaymentMethodResponse.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Payment.Responses
{
    [JsonObject]
    public sealed class StripePaymentMethodResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("card")]
        public StripePaymentMethodCardResponse StripePaymentMethodCard { get; set; }
    }
}
