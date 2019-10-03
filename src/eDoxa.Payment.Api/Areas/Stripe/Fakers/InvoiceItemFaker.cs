// Filename: InvoiceItemFaker.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Fakers
{
    public sealed class InvoiceItemFaker : Faker<InvoiceItem>
    {
        public InvoiceItem FakeInvoiceItem()
        {
            return this.Generate();
        }
    }
}
