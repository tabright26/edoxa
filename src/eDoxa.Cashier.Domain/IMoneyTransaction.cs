// Filename: IMoneyTransaction.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Functional.Maybe;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Cashier.Domain
{
    public interface IMoneyTransaction : ITransaction<Money>, IEntity<TransactionId>
    {
        Option<MoneyTransaction> TryPayoff(Money amount);
    }
}