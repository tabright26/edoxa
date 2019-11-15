// Filename: TransactionService.cs
// Date Created: 2019-11-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Areas.Transactions.Services.Abstractions;
using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain.Miscs;

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

        public async Task MaskTransactionAsSuccededAsync(ITransaction transaction, CancellationToken cancellationToken = default)
        {
            transaction.MarkAsSucceded();

            await _transactionRepository.CommitAsync(cancellationToken);
        }

        public async Task MaskTransactionAsFailedAsync(ITransaction transaction, CancellationToken cancellationToken = default)
        {
            transaction.MarkAsFailed();

            await _transactionRepository.CommitAsync(cancellationToken);
        }

        public async Task MaskTransactionAsCanceledAsync(ITransaction transaction, CancellationToken cancellationToken = default)
        {
            transaction.MarkAsCanceled();

            await _transactionRepository.CommitAsync(cancellationToken);
        }
    }
}
