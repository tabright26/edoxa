// Filename: IAccountToken.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface IAccountToken : IAccount<Token>
    {
        ITransaction Reward(Token amount);
    }
}
