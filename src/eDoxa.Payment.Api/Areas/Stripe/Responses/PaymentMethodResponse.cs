// Filename: PaymentMethodResponse.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.Areas.Stripe.Responses
{
    [JsonObject]
    public sealed class PaymentMethodResponse
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("type")]
        public string Type { get; set; }

        [JsonProperty("card")]
        public CardResponse Card { get; set; }
    }
}
