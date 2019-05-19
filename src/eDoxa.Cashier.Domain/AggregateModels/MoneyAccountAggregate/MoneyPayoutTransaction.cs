// Filename: MoneyPayoutTransaction.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    public sealed class MoneyPayoutTransaction : MoneyTransaction
    {
        public MoneyPayoutTransaction(Money amount) : base(amount, new TransactionDescription(nameof(MoneyPayoutTransaction)), TransactionType.Payout)
        {
        }
    }
}
