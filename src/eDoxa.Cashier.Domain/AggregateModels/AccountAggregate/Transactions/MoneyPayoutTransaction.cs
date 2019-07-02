﻿// Filename: MoneyPayoutTransaction.cs
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
    public sealed class MoneyPayoutTransaction : Transaction
    {
        public MoneyPayoutTransaction(Money currency) : base(
            currency,
            new TransactionDescription(nameof(MoneyPayoutTransaction)),
            TransactionType.Payout,
            new UtcNowDateTimeProvider()
        )
        {
        }
    }
}
