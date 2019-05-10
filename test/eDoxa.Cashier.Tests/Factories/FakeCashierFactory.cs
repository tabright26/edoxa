// Filename: UserAggregateFactory.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services.Stripe;
using eDoxa.Seedwork.Factories;

using Stripe;

using Token = eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate.Token;

namespace eDoxa.Cashier.Tests.Factories
{
    public sealed partial class FakeCashierFactory
    {
        private static readonly Lazy<FakeCashierFactory> Lazy =
            new Lazy<FakeCashierFactory>(() => new FakeCashierFactory());

        public static FakeCashierFactory Instance => Lazy.Value;
    }

    public sealed partial class FakeCashierFactory
    {
        public UserId CreateUserId()
        {
            return UserId.FromGuid(Guid.NewGuid());
        }

        public CustomerId CreateCustomerId()
        {
            return CustomerId.Parse("cus_TrgePgEEYXHkAt");
        }

        public CardId CreateCardId()
        {
            return CardId.Parse("card_gePgEwe23HkAt");
        }
    }

    public sealed partial class FakeCashierFactory
    {
        public Money CreateMoney()
        {
            return Money.Zero;
        }

        public Token CreateToken()
        {
            return Token.Zero;
        }

        public Card CreateCard()
        {
            var customer = this.CreateCustomer();

            var address = customer.Shipping.Address;

            return new Card
            {
                Id = this.CreateCardId().ToString(),
                CustomerId = customer.Id,
                AddressCity = address.City,
                AddressCountry = address.Country,
                AddressLine1 = address.Line1,
                AddressLine2 = address.Line2,
                AddressZip = address.PostalCode,
                AddressState = address.State
            };
        }

        public Customer CreateCustomer()
        {
            return new Customer
            {
                Id = this.CreateCustomerId().ToString(),
                Email = "customer@edoxa.gg",
                Shipping = new Shipping
                {
                    Phone = "5555555555",
                    Address = new Address
                    {
                        City = "Montreal",
                        Country = "CA",
                        Line1 = "200 Notre-Dame Ouest",
                        Line2 = null,
                        PostalCode = "J3L 3Y8",
                        State = "QC"
                    }
                }
            };
        }
    }
}