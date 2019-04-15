// Filename: TokenBundle.cs
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
    public sealed class TokenBundle : CurrencyBundle<Token>
    {
        public TokenBundle(Money price, Token amount) : base(price, amount)
        {
        }

        public override InvoiceItemCreateOptions BuildInvoiceItem(CustomerId customerId)
        {
            return new InvoiceItemCreateOptions
            {
                CustomerId = customerId.ToString(),
                Description = $"eDoxa Tokens ({Amount})",
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