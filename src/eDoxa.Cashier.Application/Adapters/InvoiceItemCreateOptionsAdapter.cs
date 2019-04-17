// Filename: InvoiceItemCreateOptionsAdapter.cs
// Date Created: 2019-04-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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
            _transaction = transaction;
        }

        public InvoiceItemCreateOptions InvoiceItemCreateOptions =>
            new InvoiceItemCreateOptions
            {
                CustomerId = _transaction.User.CustomerId.ToString(),
                Description = _transaction.Description.ToString(),
                Amount = _transaction.Price.AsCents(),
                Currency = Currency
            };
    }
}