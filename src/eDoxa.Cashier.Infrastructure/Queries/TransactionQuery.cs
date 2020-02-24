// Filename: TransactionQuery.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure.Extensions;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Queries
{
    public sealed partial class TransactionQuery
    {
        public TransactionQuery(CashierDbContext cashierDbContext)
        {
            Transactions = cashierDbContext.Set<TransactionModel>().AsNoTracking();
        }

        private IQueryable<TransactionModel> Transactions { get; }

        private async Task<IReadOnlyCollection<TransactionModel>> FetchTransactionModelsAsync(
            Guid userId,
            int? currency = null,
            int? type = null,
            int? status = null
        )
        {
            var transactions = from transaction in Transactions.Include(account => account.Account).AsExpandable()
                               where transaction.Account.Id == userId &&
                                     (currency == null || transaction.Currency == currency) &&
                                     (type == null || transaction.Type == type) &&
                                     (status == null || transaction.Status == status) &&
                                     transaction.Status != TransactionStatus.Deleted.Value
                               orderby transaction.Timestamp descending
                               select transaction;

            return await transactions.ToListAsync();
        }

        private async Task<TransactionModel?> FindTransactionModelAsync(Guid transactionId)
        {
            var transactions = from transaction in Transactions.AsExpandable()
                               where transaction.Id == transactionId
                               select transaction;

            return await transactions.SingleOrDefaultAsync();
        }
    }

    public sealed partial class TransactionQuery : ITransactionQuery
    {
        public async Task<IReadOnlyCollection<ITransaction>> FetchUserTransactionsAsync(
            UserId userId,
            CurrencyType? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        )
        {
            var transactionModels = await this.FetchTransactionModelsAsync(
                userId,
                currency?.Value,
                type?.Value,
                status?.Value);

            return transactionModels.Select(model => model.ToEntity()).ToList();
        }

        public async Task<ITransaction?> FindTransactionAsync(TransactionId transactionId)
        {
            var transactionModel = await this.FindTransactionModelAsync(transactionId);

            return transactionModel?.ToEntity();
        }
    }
}
