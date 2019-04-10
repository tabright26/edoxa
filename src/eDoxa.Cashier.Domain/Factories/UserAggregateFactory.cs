// Filename: UserAggregateFactory.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Factories;

using Stripe;

using Token = eDoxa.Cashier.Domain.AggregateModels.UserAggregate.Token;

namespace eDoxa.Cashier.Domain.Factories
{
    public sealed partial class UserAggregateFactory : AggregateFactory
    {
        private static readonly Lazy<UserAggregateFactory> _lazy = new Lazy<UserAggregateFactory>(() => new UserAggregateFactory());

        public static UserAggregateFactory Instance
        {
            get
            {
                return _lazy.Value;
            }
        }
    }

    public sealed partial class UserAggregateFactory
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

    public sealed partial class UserAggregateFactory
    {
        public User CreateAdmin()
        {
            return User.Create(AdminData);
        }

        public User CreateFrancis()
        {
            return User.Create(FrancisData);
        }

        public User CreateRoy()
        {
            return User.Create(RoyData);
        }

        public User CreateRyan()
        {
            return User.Create(RyanData);
        }

        public User CreateUser()
        {
            return User.Create(this.CreateUserId(), this.CreateCustomerId());
        }

        public Money CreateMoney()
        {
            return Money.Empty;
        }

        public Token CreateToken()
        {
            return Token.Empty;
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