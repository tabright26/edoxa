// Filename: AccountFaker.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Fakers
{
    public sealed class AccountFaker : Faker<Account>
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
