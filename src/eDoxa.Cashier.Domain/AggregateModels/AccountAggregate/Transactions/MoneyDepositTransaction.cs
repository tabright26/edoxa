// Filename: MoneyDepositTransaction.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate.Transactions
{
    public sealed class MoneyDepositTransaction : Transaction
    {
        public MoneyDepositTransaction(Money amount) : base(amount, new TransactionDescription(nameof(MoneyDepositTransaction)), TransactionType.Deposit)
        {
        }
    }
}
