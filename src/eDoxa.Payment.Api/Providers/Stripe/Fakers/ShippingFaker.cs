// Filename: ShippingFaker.cs
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
    public sealed class ShippingFaker : Faker<Shipping>
    {
        private readonly AddressFaker _addressFaker = new AddressFaker();

        public ShippingFaker()
        {
            this.RuleFor(shipping => shipping.Phone, faker => faker.Phone.PhoneNumber("##########"));

            this.RuleFor(shipping => shipping.Address, _addressFaker);
        }

        public Shipping FakeShipping()
        {
            return this.Generate();
        }
    }
}
