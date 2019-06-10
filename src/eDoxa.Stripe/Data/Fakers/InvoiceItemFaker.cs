// Filename: InvoiceItemFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Seedwork.Common.Extensions;

using Stripe;

namespace eDoxa.Stripe.Data.Fakers
{
    public sealed class InvoiceItemFaker : Faker<InvoiceItem>
    {
        public InvoiceItemFaker()
        {
            this.UseSeed();
        }

        public InvoiceItem FakeInvoiceItem()
        {
            return this.Generate();
        }
    }
}
