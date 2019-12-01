// Filename: StripePaymentMethodAttachPostRequest.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Payment.Requests
{
    [JsonObject]
    public sealed class StripePaymentMethodAttachPostRequest
    {
        [JsonConstructor]
        public StripePaymentMethodAttachPostRequest(bool defaultPaymentMethod = false)
        {
            DefaultPaymentMethod = defaultPaymentMethod;
        }

        public StripePaymentMethodAttachPostRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("defaultPaymentMethod", Required = Required.AllowNull)]
        public bool DefaultPaymentMethod { get; private set; }
    }
}
