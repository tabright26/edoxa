// Filename: InvoiceItemCreateOptionsAdapter.cs
// Date Created: 2019-04-15
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;

using Stripe;

namespace eDoxa.Cashier.Application.Adapters
{
    public sealed class InvoiceItemCreateOptionsAdapter
    {
        private const string Currency = "usd";

        private readonly Transaction _transaction;

        public InvoiceItemCreateOptionsAdapter(Transaction transaction)
        {
            _transaction = transaction ?? throw new ArgumentNullException(nameof(transaction));
        }

        public InvoiceItemCreateOptions InvoiceItemCreateOptions
        {
            get
            {
                return new InvoiceItemCreateOptions
                {
                    CustomerId = _transaction.CustomerId.ToString(),
                    Description = _transaction.Description,
                    Amount = _transaction.Price.AsCents(),
                    Currency = Currency
                };
            }
        }
    }
}