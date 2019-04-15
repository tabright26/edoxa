// Filename: MoneyBundle.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using Stripe;

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class MoneyBundle : CurrencyBundle<Money>
    {
        public MoneyBundle(Money amount) : base(amount, amount)
        {
        }

        public override InvoiceItemCreateOptions BuildInvoiceItem(CustomerId customerId)
        {
            return new InvoiceItemCreateOptions
            {
                CustomerId = customerId.ToString(),
                Description = $"eDoxa Funds ({Amount})",
                Amount = Price.AsCents(),
                Currency = "usd",
                Metadata = new Dictionary<string, string>
                {
                    ["Type"] = Amount.Type, ["Amount"] = Amount.ToString()
                }
            };
        }
    }
}