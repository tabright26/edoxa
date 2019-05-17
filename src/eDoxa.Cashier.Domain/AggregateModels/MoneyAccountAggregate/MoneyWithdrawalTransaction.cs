// Filename: WithdrawalMoneyTransaction.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate
{
    public class MoneyWithdrawalTransaction : MoneyTransaction
    {
        public MoneyWithdrawalTransaction(Money amount) : base(-amount, new TransactionDescription($"Money withdrawal ({amount})"), TransactionType.Withdrawal)
        {
        }
    }
}