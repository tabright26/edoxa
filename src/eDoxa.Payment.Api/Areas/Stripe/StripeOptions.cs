// Filename: StripeOptions.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

namespace eDoxa.Payment.Api.Areas.Stripe
{
    // TODO: Add securiry attribute like [Required]
    public sealed class StripeOptions
    {
        public string Country { get; set; }

        public string Currency { get; set; }

        public string BusinessType { get; set; }

        public string AccountType { get; set; }

        public List<string> TaxRateIds { get; set; }
    }
}
