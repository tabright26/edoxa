// Filename: InvoiceFaker.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Fakers
{
    public sealed class InvoiceFaker : Faker<Invoice>
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
