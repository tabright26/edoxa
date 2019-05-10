// Filename: ITransaction.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Functional.Maybe;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface ITransaction<out TCurrency>
    where TCurrency : ICurrency
    {
        TCurrency Amount { get; }

        string ServiceId { get; }

        bool Pending { get; }

        DateTime Timestamp { get; }

        TransactionType Type { get; }

        Option<TransactionDescription> Description { get; }
    }
}