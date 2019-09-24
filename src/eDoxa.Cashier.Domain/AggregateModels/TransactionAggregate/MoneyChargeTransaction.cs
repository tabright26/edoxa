// Filename: MoneyChargeTransaction.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;

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
