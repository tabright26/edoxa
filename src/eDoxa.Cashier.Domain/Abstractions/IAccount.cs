// Filename: IAccount.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface IAccount : IEntity<AccountId>, IAggregateRoot
    {
        User User { get; }

        IReadOnlyCollection<Transaction> Transactions { get; }

        void CreateTransaction(Transaction transaction);
    }

    public interface IAccount<in TCurrency>
    where TCurrency : ICurrency
    {
        Balance Balance { get; }

        DateTime? LastDeposit { get; }

        ITransaction Deposit(TCurrency amount);

        ITransaction Charge(TCurrency amount);

        ITransaction Payout(TCurrency amount);

        ITransaction CompleteTransaction(ITransaction transaction);

        ITransaction FailureTransaction(ITransaction transaction, string message);
    }
}
