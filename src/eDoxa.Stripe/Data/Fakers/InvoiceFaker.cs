// Filename: InvoiceFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Seedwork.Common.Abstactions;

using Stripe;

namespace eDoxa.Stripe.Data.Fakers
{
    public sealed class InvoiceFaker : CustomFaker<Invoice>
    {
        public InvoiceFaker()
        {
            this.RuleFor(invoice => invoice.Id, faker => $"in_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

            this.RuleFor(invoice => invoice.Object, "invoice");

            this.RuleFor(invoice => invoice.Created, faker => faker.Date.Recent());

            this.RuleFor(invoice => invoice.Description, faker => faker.Lorem.Sentence());

            this.RuleFor(invoice => invoice.Currency, "cad");
        }

        public Invoice FakeInvoice()
        {
            return this.Generate();
        }
    }
}
