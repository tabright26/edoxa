// Filename: ITransactionService.cs
// Date Created: 2019-12-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Services
{
    public interface ITransactionService
    {
        Task<ITransaction?> FindTransactionAsync(TransactionId transactionId);

        Task<ITransaction?> FindTransactionAsync(TransactionMetadata metadata);

        Task MarkTransactionAsSuccededAsync(ITransaction transaction, CancellationToken cancellationToken = default);

        Task MarkTransactionAsFailedAsync(ITransaction transaction, CancellationToken cancellationToken = default);

        Task MarkTransactionAsCanceledAsync(ITransaction transaction, CancellationToken cancellationToken = default);
    }
}
