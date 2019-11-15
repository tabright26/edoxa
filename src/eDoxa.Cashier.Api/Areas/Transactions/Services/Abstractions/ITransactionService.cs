// Filename: ITransactionService.cs
// Date Created: 2019-11-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Cashier.Api.Areas.Transactions.Services.Abstractions
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
