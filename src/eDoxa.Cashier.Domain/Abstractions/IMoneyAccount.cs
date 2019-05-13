﻿// Filename: IMoneyAccount.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Functional;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface IMoneyAccount : IAccount<Money, IMoneyTransaction>, IEntity<AccountId>
    {
        DateTime? LastWithdrawal { get; }

        IReadOnlyCollection<MoneyTransaction> Transactions { get; }

        Option<IMoneyTransaction> TryWithdrawal(Money amount);
    }
}