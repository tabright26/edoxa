// Filename: IAccount.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.Domain
{
    public interface IAccount<TCurrency, TTransaction>
    where TCurrency : ICurrency
    where TTransaction : ITransaction<TCurrency>
    {
        TCurrency Balance { get; }

        TCurrency Pending { get; }

        TTransaction Deposit(TCurrency amount);

        Option<TTransaction> TryRegister(TCurrency amount, ServiceId serviceId);

        Option<TTransaction> TryPayoff(TCurrency amount, ServiceId serviceId);
    }
}