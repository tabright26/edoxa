// Filename: ITransaction.cs
// Date Created: 2019-05-09
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
    public interface ITransaction<out TCurrency>
    where TCurrency : ICurrency
    {
        string ServiceId { get; }

        DateTime Timestamp { get; }

        TCurrency Amount { get; }

        TransactionDescription Description { get; }

        TransactionType Type { get; }

        TransactionStatus Status { get; }

        void Pay();

        void Cancel();

        void Fail();
    }
}