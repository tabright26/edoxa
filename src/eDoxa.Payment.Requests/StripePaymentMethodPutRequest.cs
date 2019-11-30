// Filename: StripePaymentMethodPutRequest.cs
// Date Created: 2019-10-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Payment.Requests
{
    [DataContract]
    public sealed class StripePaymentMethodPutRequest
    {
        public StripePaymentMethodPutRequest(long expMonth, long expYear)
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
