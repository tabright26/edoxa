// Filename: AddressFaker.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Fakers
{
    public sealed class AddressFaker : Faker<Address>
    {
        public AddressFaker()
        {
            this.RuleFor(address => address.Line1, faker => faker.Address.StreetAddress());

            this.Ignore(address => address.Line2);

            this.RuleFor(address => address.City, faker => faker.Address.City());

            this.RuleFor(address => address.State, faker => faker.Address.State());

            this.RuleFor(address => address.PostalCode, faker => faker.Address.ZipCode());

            this.RuleFor(address => address.Country, faker => faker.Address.Country());
        }

        public Address FakeAddress()
        {
            return this.Generate();
        }
    }
}
