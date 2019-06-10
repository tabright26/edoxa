// Filename: AccountFaker.cs
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
    public sealed class AccountFaker : Faker<Account>
    {
        private readonly PersonFaker _personFaker = new PersonFaker();

        public AccountFaker()
        {
            this.UseSeed();

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
