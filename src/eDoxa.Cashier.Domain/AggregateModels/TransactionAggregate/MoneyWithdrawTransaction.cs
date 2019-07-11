﻿// Filename: MoneyWithdrawTransaction.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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
