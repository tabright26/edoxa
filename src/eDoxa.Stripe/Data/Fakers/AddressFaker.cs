// Filename: AddressFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using Stripe;

namespace eDoxa.Stripe.Data.Fakers
{
    public sealed class AddressFaker : Faker<Address>
    {
        public AddressFaker() : base("en_CA")
        {
            this.UseSeed(8675309);

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
