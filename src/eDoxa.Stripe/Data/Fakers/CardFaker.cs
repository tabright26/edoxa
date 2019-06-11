// Filename: CardFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Seedwork.Common.Abstactions;

using Stripe;

namespace eDoxa.Stripe.Data.Fakers
{
    public sealed class CardFaker : CustomFaker<Card>
    {
        private readonly AddressFaker _addressFaker = new AddressFaker();
        private readonly CustomerFaker _customerFaker = new CustomerFaker();

        public CardFaker()
        {
            this.RuleFor(customer => customer.Id, faker => $"card_{faker.Random.Guid().ToString().Replace("-", string.Empty)}");

            this.RuleFor(customer => customer.Object, "card");

            this.RuleFor(customer => customer.CustomerId, _customerFaker.Generate().Id);

            this.FinishWith(
                (faker, card) =>
                {
                    var address = _addressFaker.Generate();

                    card.AddressCity = address.City;

                    card.AddressCountry = address.Country;

                    card.AddressLine1 = address.Line1;

                    card.AddressLine2 = address.Line2;

                    card.AddressZip = address.PostalCode;

                    card.AddressState = address.State;
                }
            );
        }

        public StripeList<Card> FakeCards(int count)
        {
            return new StripeList<Card>
            {
                Data = this.Generate(count)
            };
        }

        public Card FakeCard()
        {
            return this.Generate();
        }
    }
}
