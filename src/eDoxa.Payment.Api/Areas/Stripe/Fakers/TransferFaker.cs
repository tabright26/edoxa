// Filename: TransferFaker.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Fakers
{
    public sealed class TransferFaker : Faker<Transfer>
    {
        public TransferFaker()
        {
            this.RuleFor(transfer => transfer.Id, faker => $"tr_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

            this.RuleFor(transfer => transfer.Object, "transfer");

            this.RuleFor(transfer => transfer.Created, faker => faker.Date.Recent());

            this.RuleFor(transfer => transfer.Description, faker => faker.Lorem.Sentence());

            this.RuleFor(transfer => transfer.DestinationId, faker => $"acct_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

            this.RuleFor(transfer => transfer.Currency, "cad");
        }

        public Transfer FakeTransfer()
        {
            return this.Generate();
        }
    }
}
