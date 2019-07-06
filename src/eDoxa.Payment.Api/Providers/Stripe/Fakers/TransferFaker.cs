// Filename: TransferFaker.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Providers.Stripe.Fakers
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
