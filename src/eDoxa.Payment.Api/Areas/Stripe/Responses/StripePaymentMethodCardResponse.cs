// Filename: StripePaymentMethodCardResponse.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.Areas.Stripe.Responses
{
    [JsonObject]
    public sealed class StripePaymentMethodCardResponse
    {
        [JsonProperty("brand")]
        public string Brand { get; set; }

        [JsonProperty("country")]
        public string Country { get; set; }

        [JsonProperty("expMonth")]
        public long ExpMonth { get; set; }

        [JsonProperty("expYear")]
        public long ExpYear { get; set; }

        [JsonProperty("last4")]
        public string Last4 { get; set; }
    }
}
