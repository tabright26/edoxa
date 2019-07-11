// Filename: MoneyDepositTransaction.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Providers;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public sealed class MoneyDepositTransaction : Transaction
    {
        public MoneyDepositTransaction(Money currency) : base(
            currency,
            new TransactionDescription(nameof(MoneyDepositTransaction)),
            TransactionType.Deposit,
            new UtcNowDateTimeProvider()
        )
        {
        }
    }
}
