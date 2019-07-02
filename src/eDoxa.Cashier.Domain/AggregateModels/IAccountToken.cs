// Filename: IAccountToken.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

namespace eDoxa.Cashier.Domain.AggregateModels
{
    public interface IAccountToken : IAccount<Token>
    {
        ITransaction Reward(Token amount);
    }
}
