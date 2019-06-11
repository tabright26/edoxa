// Filename: BankAccountFaker.cs
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
    public sealed class BankAccountFaker : CustomFaker<BankAccount>
    {
        public BankAccountFaker()
        {
            this.RuleFor(customer => customer.Id, faker => $"ba_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

            this.RuleFor(customer => customer.Object, "bank_account");
        }

        public BankAccount FakeBankAccount()
        {
            return this.Generate();
        }
    }
}
