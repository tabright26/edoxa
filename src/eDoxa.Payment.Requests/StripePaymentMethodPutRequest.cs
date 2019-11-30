// Filename: StripePaymentMethodPutRequest.cs
// Date Created: 2019-11-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Newtonsoft.Json;

namespace eDoxa.Payment.Requests
{
    [JsonObject]
    public sealed class StripePaymentMethodPutRequest
    {
        [JsonConstructor]
        public StripePaymentMethodPutRequest(long expMonth, long expYear)
        {
            ExpMonth = expMonth;
            ExpYear = expYear;
        }

        public StripePaymentMethodPutRequest()
        {
            // Required by Fluent Validation.
        }

        [JsonProperty("expMonth")]
        public long ExpMonth { get; private set; }

        [JsonProperty("expYear")]
        public long ExpYear { get; private set; }
    }
}
