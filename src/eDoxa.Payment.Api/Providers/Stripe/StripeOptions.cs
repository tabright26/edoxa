// Filename: StripeOptions.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

#nullable disable

using System.Collections.Generic;

namespace eDoxa.Payment.Api.Providers.Stripe
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
