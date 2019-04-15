// Filename: Transaction.cs
// Date Created: 2019-04-15
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.UserAggregate
{
    public sealed class Transaction
    {
        public static readonly decimal TaxPercent = 15M;

        public Transaction(CustomerId customerId, string description, Money price)
        {
            CustomerId = customerId;
            Description = description;
            Price = price;
        }

        public CustomerId CustomerId { get; }

        public string Description { get; }

        public Money Price { get; }
    }
}