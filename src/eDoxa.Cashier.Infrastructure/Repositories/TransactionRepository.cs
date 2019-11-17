// Filename: TransactionRepository.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class TransactionRepository
    {
        private readonly IDictionary<Guid, ITransaction> _materializedIds = new Dictionary<Guid, ITransaction>();
        private readonly IDictionary<ITransaction, TransactionModel> _materializedObjects = new Dictionary<ITransaction, TransactionModel>();
        private readonly CashierDbContext _context;
        private readonly IMapper _mapper;

        public TransactionRepository(CashierDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<TransactionModel?> FindTransactionModelAsync(Guid transactionId)
        {
            var transactionModels = from transaction in _context.Transactions.AsExpandable()
                                    where transaction.Id == transactionId
                                    select transaction;

            return await transactionModels.SingleOrDefaultAsync();
        }

        public async Task<TransactionModel?> FindTransactionModelAsync(IDictionary<string, string> metadata)
        {
            var transactionModels = from transaction in _context.Transactions.AsExpandable()
                                    where transaction.Metadata.Any(item => metadata.Contains(new KeyValuePair<string, string>(item.Key, item.Value)))
                                    select transaction;

            return await transactionModels.SingleOrDefaultAsync();
        }
    }

    public sealed partial class TransactionRepository : ITransactionRepository
    {
        public async Task<ITransaction?> FindTransactionAsync(TransactionId transactionId)
        {
            if (_materializedIds.TryGetValue(transactionId, out var transaction))
            {
                return transaction;
            }

            var transactionModel = await this.FindTransactionModelAsync(transactionId);

            if (transactionModel == null)
            {
                return null;
            }

            transaction = _mapper.Map<ITransaction>(transactionModel);

            _materializedObjects[transaction] = transactionModel;

            _materializedIds[transactionModel.Id] = transaction;

            return transaction;
        }

        public async Task<ITransaction?> FindTransactionAsync(TransactionMetadata metadata)
        {
            var transaction = _materializedIds.Values.SingleOrDefault(
                x => x.Metadata.Any(item => metadata.Contains(new KeyValuePair<string, string>(item.Key, item.Value))));

            if (transaction != null)
            {
                return transaction;
            }

            var transactionModel = await this.FindTransactionModelAsync(metadata);

            if (transactionModel == null)
            {
                return null;
            }

            transaction = _mapper.Map<ITransaction>(transactionModel);

            _materializedObjects[transaction] = transactionModel;

            _materializedIds[transactionModel.Id] = transaction;

            return transaction;
        }

        public async Task CommitAsync(CancellationToken cancellationToken = default)
        {
            foreach (var (transaction, transactionModel) in _materializedObjects)
            {
                this.CopyChanges(transaction, transactionModel);
            }

            await _context.SaveChangesAsync(cancellationToken);

            foreach (var (transaction, transactionModel) in _materializedObjects)
            {
                _materializedIds[transactionModel.Id] = transaction;
            }
        }

        private void CopyChanges(ITransaction transaction, TransactionModel transactionModel)
        {
            transactionModel.Status = transaction.Status.Value;
        }
    }
}
