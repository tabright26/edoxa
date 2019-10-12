// Filename: IAccount.cs
// Date Created: 2019-07-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;
using eDoxa.Specifications.Abstractions;

namespace eDoxa.Cashier.Domain.AggregateModels.AccountAggregate
{
    public interface IAccount : IEntity<AccountId>, IAggregateRoot, IVerifiable
    {
        UserId UserId { get; }

        IReadOnlyCollection<ITransaction> Transactions { get; }

        void CreateTransaction(ITransaction transaction);

        Balance GetBalanceFor(Currency currency);
    }

    public interface IAccount<in TCurrency> : IVerifiable
    where TCurrency : ICurrency
    {
        Balance Balance { get; }

        DateTime? LastDeposit { get; }

        ITransaction Deposit(TCurrency amount);

        ITransaction Charge(TCurrency amount);

        ITransaction Payout(TCurrency amount);
    }
}
