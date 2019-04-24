// Filename: ITransaction.cs
// Date Created: 2019-04-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Cashier.Domain
{
    public interface ITransaction<out TCurrency>
    where TCurrency : ICurrency
    {
        DateTime Timestamp { get; }

        TCurrency Amount { get; }
    }
}