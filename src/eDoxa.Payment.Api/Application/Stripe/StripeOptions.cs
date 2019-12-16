// Filename: StripeOptions.cs
// Date Created: 2019-10-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

#nullable disable

using System.Collections.Generic;

namespace eDoxa.Payment.Api.Application.Stripe
{
    public sealed class StripeOptions
    {
        public StripeInvoiceOptions Invoice { get; set; }

        public StripeTransferOptions Transfer { get; set; }
    }

    public sealed class StripeInvoiceOptions
    {
        public string Currency { get; set; }

        public List<string> TaxRates { get; set; }
    }

    public sealed class StripeTransferOptions
    {
        public string Currency { get; set; }
    }
}
