﻿// Filename: IMoneyAccount.cs
// Date Created: 2019-04-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain
{
    public interface IMoneyAccount : IAccount<Money, IMoneyTransaction>, IEntity<AccountId>
    {
        IReadOnlyCollection<MoneyTransaction> Transactions { get; }

        Option<IMoneyTransaction> TryWithdraw(Money amount);
    }
}