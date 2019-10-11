// Filename: PaymentMethodPutRequest.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Payment.Api.Areas.Stripe.Requests
{
    [DataContract]
    public sealed class PaymentMethodPutRequest
    {
        public PaymentMethodPutRequest(long expMonth, long expYear)
        {
            ExpMonth = expMonth;
            ExpYear = expYear;
        }

        [DataMember(Name = "expMonth")]
        public long ExpMonth { get; }

        [DataMember(Name = "expYear")]
        public long ExpYear { get; }
    }
}
