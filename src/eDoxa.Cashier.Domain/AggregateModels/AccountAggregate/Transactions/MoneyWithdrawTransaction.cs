// Filename: MoneyWithdrawTransaction.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Common;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transactions
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
