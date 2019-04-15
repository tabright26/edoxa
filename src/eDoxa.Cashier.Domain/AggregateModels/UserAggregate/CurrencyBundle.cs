// Filename: CurrencyBundle.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Stripe;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public abstract class CurrencyBundle<TCurrency>
    where TCurrency : Currency<TCurrency>, new()
    {
        protected private CurrencyBundle(Money price, TCurrency amount)
        {
            Price = price;
            Amount = amount;
        }

        public TCurrency Amount { get; }

        protected Money Price { get; }

        public abstract InvoiceItemCreateOptions BuildInvoiceItem(CustomerId customerId);
    }
}