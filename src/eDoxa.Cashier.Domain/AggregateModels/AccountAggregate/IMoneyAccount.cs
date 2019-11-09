// Filename: IMoneyAccount.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Immutable;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface IMoneyAccount : IAccount<Money>
    {
        DateTime? LastWithdraw { get; }

        ITransaction Withdrawal(TransactionId transactionId, Money amount, IImmutableSet<Bundle> bundles);

        bool HaveSufficientMoney(Money money);

        bool IsDepositAvailable();

        bool IsWithdrawalAvailable();
    }
}
