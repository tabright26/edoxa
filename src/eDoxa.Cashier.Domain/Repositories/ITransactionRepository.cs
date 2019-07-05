// Filename: ITransactionRepository.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface ITransactionRepository
    {
        [ItemCanBeNull]
        Task<ITransaction> FindTransactionAsync(TransactionId transactionId);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
