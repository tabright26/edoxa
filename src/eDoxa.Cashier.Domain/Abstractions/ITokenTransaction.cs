// Filename: ITokenTransaction.cs
// Date Created: 2019-05-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain.Abstractions
{
    public interface ITokenTransaction : ITransaction<Token>, IEntity<TransactionId>
    {
    }
}