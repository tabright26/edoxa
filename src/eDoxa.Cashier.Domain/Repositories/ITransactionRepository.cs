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

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Repositories
{
    public interface ITransactionRepository
    {
        Task<ITransaction?> FindTransactionAsync(TransactionId transactionId);

        Task<ITransaction?> FindTransactionAsync(TransactionMetadata metadata);

        Task CommitAsync(CancellationToken cancellationToken = default);
    }
}
