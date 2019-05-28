// Filename: FakeStripeFactory.cs
// Date Created: 2019-05-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Services.Stripe;

using Stripe;

namespace eDoxa.Cashier.Tests.Factories
{
    public sealed class FakeStripeFactory
    {
        private static readonly Lazy<FakeStripeFactory> Lazy = new Lazy<FakeStripeFactory>(() => new FakeStripeFactory());

        public static FakeStripeFactory Instance => Lazy.Value;

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

        public string CreateSourceToken()
        {
            return "qwe23rwr2r12rqwe123qwsda241qweasd";
        }

        public Account CreateAccount()
        {
            const string email = "test@edoxa.gg";

            return new Account
            {
                Id = this.CreateAccountId().ToString(),
                Individual = this.CreatePerson(),
                Email = email,
                Country = StripeConstants.Country,
                DefaultCurrency = StripeConstants.Currency,
                BusinessType = StripeConstants.BusinessType,
                Type = StripeConstants.AccountType
            };
        }

        public Person CreatePerson()
        {
            return new Person
            {
                FirstName = "Firstname",
                LastName = "Lastname",
                Email = "test@edoxa.gg",
                Dob = new Dob
                {
                    Day = 1,
                    Month = 1,
                    Year = 2000
                },
                Address = this.CreateAddress()
            };
        }

        public Address CreateAddress()
        {
            return new Address
            {
                Line1 = "1000 Street",
                Line2 = null,
                City = "Montreal",
                State = "QC",
                PostalCode = "H5T I2E",
                Country = StripeConstants.Country
            };
        }

        public IExternalAccount CreateBankAccount()
        {
            return new BankAccount
            {
                Id = this.CreateBankAccountId().ToString(),
                Object = "bank_account"
            };
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

        public Customer CreateCustomer()
        {
            return new Customer
            {
                Id = this.CreateCustomerId().ToString(),
                Email = "test@edoxa.gg",
                Shipping = new Shipping
                {
                    Phone = "0000000000",
                    Address = this.CreateAddress()
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

        public Transfer CreateTransfer()
        {
            return new Transfer();
        }
    }
}
