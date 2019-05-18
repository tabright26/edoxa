// Filename: ITokenAccount.cs
// Date Created: 2019-05-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface ITokenAccount : IAccount<Token, ITokenTransaction>, IEntity<AccountId>
    {
        IReadOnlyCollection<TokenTransaction> Transactions { get; }

        ITokenTransaction Reward(Token amount);
    }
}