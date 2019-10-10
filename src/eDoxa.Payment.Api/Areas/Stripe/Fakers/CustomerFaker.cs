// Filename: CustomerFaker.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using Stripe;

namespace eDoxa.Payment.Api.Areas.Stripe.Fakers
{
    public sealed class CustomerFaker : Faker<Customer>
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
