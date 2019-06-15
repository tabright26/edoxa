// Filename: TransferFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common.Abstactions;

using Stripe;

namespace eDoxa.Stripe.Data.Fakers
{
    public sealed class TransferFaker : CustomFaker<Transfer>
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
