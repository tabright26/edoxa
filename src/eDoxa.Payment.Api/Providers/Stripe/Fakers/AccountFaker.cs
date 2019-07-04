// Filename: AccountFaker.cs
// Date Created: 2019-07-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common.Abstactions;

using Stripe;

namespace eDoxa.Payment.Api.Providers.Stripe.Fakers
{
    public sealed class AccountFaker : CustomFaker<Account>
    {
        private readonly PersonFaker _personFaker = new PersonFaker();

        public AccountFaker()
        {
            this.RuleFor(account => account.Id, faker => $"acct_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

            this.RuleFor(account => account.Individual, _personFaker);

            this.RuleFor(account => account.Email, faker => faker.Internet.Email());

            this.RuleFor(account => account.Country, faker => "CA");

            this.RuleFor(account => account.DefaultCurrency, "cad");

            this.RuleFor(account => account.BusinessType, "individual");

            this.RuleFor(account => account.Type, "custom");
        }

        public Account FakeAccount()
        {
            return this.Generate();
        }
    }
}
