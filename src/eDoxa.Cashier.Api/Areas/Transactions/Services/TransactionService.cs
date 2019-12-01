// Filename: TransactionService.cs
// Date Created: 2019-11-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Transactions.Services.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Areas.Transactions.Services
{
    public sealed class TransactionService : ITransactionService
    {
        private readonly ITransactionRepository _transactionRepository;

        public TransactionService(ITransactionRepository transactionRepository)
        {
            _transactionRepository = transactionRepository;
        }

        public async Task<ITransaction?> FindTransactionAsync(TransactionId transactionId)
        {
            return await _transactionRepository.FindTransactionAsync(transactionId);
        }

        public async Task<ITransaction?> FindTransactionAsync(TransactionMetadata metadata)
        {
            return await _transactionRepository.FindTransactionAsync(metadata);
        }

        public async Task MarkTransactionAsSuccededAsync(ITransaction transaction, CancellationToken cancellationToken = default)
        {
            transaction.MarkAsSucceded();

            await _transactionRepository.CommitAsync(cancellationToken);
        }

        public async Task MarkTransactionAsFailedAsync(ITransaction transaction, CancellationToken cancellationToken = default)
        {
            transaction.MarkAsFailed();

            await _transactionRepository.CommitAsync(cancellationToken);
        }

        public async Task MarkTransactionAsCanceledAsync(ITransaction transaction, CancellationToken cancellationToken = default)
        {
            transaction.MarkAsCanceled();

            await _transactionRepository.CommitAsync(cancellationToken);
        }
    }
}
