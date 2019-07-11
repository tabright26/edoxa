// Filename: MoneyWithdrawTransaction.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain.Providers;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public class MoneyWithdrawTransaction : Transaction
    {
        public MoneyWithdrawTransaction(Money amount) : base(
            -amount,
            new TransactionDescription(nameof(MoneyWithdrawTransaction)),
            TransactionType.Withdrawal,
            new UtcNowDateTimeProvider()
        )
        {
        }
    }
}
