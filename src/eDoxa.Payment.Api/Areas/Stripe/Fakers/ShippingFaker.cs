// Filename: ShippingFaker.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Fakers
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
