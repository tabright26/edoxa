// Filename: IAccount.cs
// Date Created: 2019-07-01
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
using eDoxa.Seedwork.Common.Abstactions;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public interface IAccount : IEntity<AccountId>, IAggregateRoot
    {
        UserId UserId { get; }

        IReadOnlyCollection<ITransaction> Transactions { get; }

        void CreateTransaction(ITransaction transaction);

        Balance GetBalanceFor(CurrencyType currency);
    }

    public interface IAccount<in TCurrency>
    where TCurrency : ICurrency
    {
        Balance Balance { get; }

        DateTime? LastDeposit { get; }

        ITransaction Deposit(TCurrency amount);

        ITransaction Charge(TCurrency amount);

        ITransaction Payout(TCurrency amount);
    }
}
