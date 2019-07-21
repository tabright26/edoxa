// Filename: IMoneyAccount.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface IMoneyAccount : IAccount<Money>
    {
        DateTime? LastWithdraw { get; }

        ITransaction Withdrawal(Money amount);
    }
}
