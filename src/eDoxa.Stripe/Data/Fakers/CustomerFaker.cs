// Filename: CustomerFaker.cs
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
    public sealed class CustomerFaker : CustomFaker<Customer>
    {
        private readonly ShippingFaker _shippingFaker = new ShippingFaker();

        public CustomerFaker()
        {
            this.RuleFor(customer => customer.Id, faker => $"cus_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

            this.RuleFor(customer => customer.Object, "customer");

            this.RuleFor(customer => customer.Email, faker => faker.Internet.Email());

            this.RuleFor(shipping => shipping.Shipping, _shippingFaker);
        }

        public Customer FakeCustomer()
        {
            return this.Generate();
        }
    }
}
