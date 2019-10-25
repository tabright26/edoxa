// Filename: StripeCustomerResponse.cs
// Date Created: 2019-10-23
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using Newtonsoft.Json;

namespace eDoxa.Payment.Api.Areas.Stripe.Responses
{
    [JsonObject]
    public sealed class StripeCustomerResponse
    {
        [JsonProperty("defaultPaymentMethodId")]
        public string DefaultPaymentMethodId { get; set; }
    }
}
