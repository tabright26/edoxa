// Filename: IAccount.cs
// Date Created: 2019-04-24
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

namespace eDoxa.Cashier.Domain
{
    public interface IAccount<TCurrency>
    where TCurrency : ICurrency
    {
        TCurrency Balance { get; }

        TCurrency Pending { get; }

        void AddBalance(TCurrency amount);

        void AddPending(TCurrency amount);

        void SubtractBalance(TCurrency amount);

        void SubtractPending(TCurrency amount);
    }
}