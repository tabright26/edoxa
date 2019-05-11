// Filename: IAccount.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Functional;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface IAccount<TCurrency, TTransaction>
    where TCurrency : ICurrency
    where TTransaction : ITransaction<TCurrency>
    {
        TCurrency Balance { get; }

        TCurrency Pending { get; }

        TTransaction Deposit(TCurrency amount);

        Option<TTransaction> TryRegister(TCurrency amount);

        Option<TTransaction> TryPayout(TCurrency amount);
    }
}