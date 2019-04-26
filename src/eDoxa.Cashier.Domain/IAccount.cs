// Filename: IAccount.cs
// Date Created: 2019-04-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;

namespace eDoxa.Cashier.Domain
{
    public interface IAccount<TCurrency>
    where TCurrency : ICurrency
    {
        TCurrency Balance { get; }

        TCurrency Pending { get; }

        ITransaction<TCurrency> Deposit(TCurrency amount);

        ITransaction<TCurrency> Register(TCurrency amount, ActivityId activityId);

        ITransaction<TCurrency> Payoff(TCurrency amount, ActivityId activityId);
    }
}