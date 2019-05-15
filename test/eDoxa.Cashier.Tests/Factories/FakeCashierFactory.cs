// Filename: FakeCashierFactory.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Domain.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Services.Stripe.Models;

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
        public StripeAccountId CreateAccountId()
        {
            return new StripeAccountId("acct_123gEwe23HkAt");
        }

        public StripeBankAccountId CreateBankAccountId()
        {
            return new StripeBankAccountId("ba_gePgEwe23HkAt");
        }

        public StripeCardId CreateCardId()
        {
            return new StripeCardId("card_gePgEwe23HkAt");
        }

        public StripeCustomerId CreateCustomerId()
        {
            return new StripeCustomerId("cus_TrgePgEEYXHkAt");
        }

        public IBundle CreateBundle()
        {
            return new MoneyBundle(this.CreateMoney());
        }

        public ITransaction CreateTransaction()
        {
            return new DepositMoneyTransaction(this.CreateMoney());
        }

        public string CreateSourceToken()
        {
            return "qwe23rwr2r12rqwe123qwsda241qweasd";
        }

        public Money CreateMoney()
        {
            return Money.OneHundred;
        }

        public Token CreateToken()
        {
            return Token.OneHundredThousand;
        }

        public Card CreateCard()
        {
            var customer = this.CreateCustomer();

            var address = customer.Shipping.Address;

            return new Card
            {
                Id = this.CreateCardId().ToString(),
                Object = "card",
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
                },
                DefaultSourceId = this.CreateCardId().ToString(),
                DefaultSource = new Card
                {
                    Object = "card"
                }
            };
        }
        
        public Invoice CreateInvoice()
        {
            return new Invoice();
        }

        public InvoiceItem CreateInvoiceItem()
        {
            return new InvoiceItem();
        }

        public StripeList<Card> CreateCards()
        {
            return new StripeList<Card>
            {
                Data = new List<Card>
                {
                    this.CreateCard()
                }
            };
        }
    }
}