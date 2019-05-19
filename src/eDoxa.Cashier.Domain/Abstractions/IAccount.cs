// Filename: IAccount.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface IAccount<TCurrency, TTransaction>
    where TCurrency : ICurrency
    where TTransaction : ITransaction<TCurrency>
    {
        TCurrency Balance { get; }

        TCurrency Pending { get; }

        DateTime? LastDeposit { get; }

        TTransaction Deposit(TCurrency amount);

        TTransaction Charge(TCurrency amount);

        TTransaction Payout(TCurrency amount);

        TTransaction CompleteTransaction(TTransaction transaction);

        TTransaction FailureTransaction(TTransaction transaction, string message);
    }
}
