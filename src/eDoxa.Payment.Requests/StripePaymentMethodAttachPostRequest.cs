// Filename: StripePaymentMethodAttachPostRequest.cs
// Date Created: 2019-10-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Runtime.Serialization;

namespace eDoxa.Payment.Requests
{
    [DataContract]
    public sealed class StripePaymentMethodAttachPostRequest
    {
        public StripePaymentMethodAttachPostRequest(bool defaultPaymentMethod = false)
        {
            DefaultPaymentMethod = defaultPaymentMethod;
        }

        [DataMember(Name = "defaultPaymentMethod", IsRequired = false)]
        public bool DefaultPaymentMethod { get; }
    }
}
