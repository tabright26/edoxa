// Filename: ITokenAccount.cs
// Date Created: 2019-04-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain
{
    public interface ITokenAccount : IAccount<Token, ITokenTransaction>, IEntity<AccountId>
    {
        IReadOnlyCollection<TokenTransaction> Transactions { get; }
    }
}