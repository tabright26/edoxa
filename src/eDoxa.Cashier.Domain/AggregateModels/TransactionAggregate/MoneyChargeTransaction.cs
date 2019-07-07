// Filename: MoneyChargeTransaction.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class MoneyChargeTransaction : Transaction
    {
        public MoneyChargeTransaction(Money currency) : base(
            -currency,
            new TransactionDescription(nameof(MoneyChargeTransaction)),
            TransactionType.Charge,
            new UtcNowDateTimeProvider()
        )
        {
        }
    }
}
