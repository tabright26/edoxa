// Filename: MoneyWithdrawTransaction.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate
{
    public class MoneyWithdrawTransaction : Transaction
    {
        public MoneyWithdrawTransaction(TransactionId transactionId, Money amount) : base(
            transactionId,
            -amount,
            new TransactionDescription(nameof(MoneyWithdrawTransaction)),
            TransactionType.Withdrawal,
            new UtcNowDateTimeProvider()
        )
        {
        }
    }
}
