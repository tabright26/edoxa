// Filename: TransactionQuery.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.TransactionAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Domain.Miscs;

using LinqKit;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Api.Infrastructure.Queries
{
    public sealed partial class TransactionQuery
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public TransactionQuery(CashierDbContext cashierDbContext, IHttpContextAccessor httpContextAccessor, IMapper mapper)
        {
            _httpContextAccessor = httpContextAccessor;
            Mapper = mapper;
            Transactions = cashierDbContext.Transactions.AsNoTracking();
        }

        private IQueryable<TransactionModel> Transactions { get; }

        public IMapper Mapper { get; }

        private async Task<IReadOnlyCollection<TransactionModel>> FetchTransactionModelsAsync(
            Guid userId,
            int? currency = null,
            int? type = null,
            int? status = null
        )
        {
            var transactions = from transaction in Transactions.Include(account => account.Account).AsExpandable()
                               where transaction.Account.UserId == userId &&
                                     (currency == null || transaction.Currency == currency) &&
                                     (type == null || transaction.Type == type) &&
                                     (status == null || transaction.Status == status)
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
            Currency? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        )
        {
            var userId = _httpContextAccessor.GetUserId();

            return await this.FetchUserTransactionsAsync(
                userId,
                currency,
                type,
                status);
        }

        public async Task<IReadOnlyCollection<ITransaction>> FetchUserTransactionsAsync(
            UserId userId,
            Currency? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        )
        {
            var transactionModels = await this.FetchTransactionModelsAsync(
                userId,
                currency?.Value,
                type?.Value,
                status?.Value);

            return Mapper.Map<IReadOnlyCollection<ITransaction>>(transactionModels);
        }

        public async Task<ITransaction?> FindTransactionAsync(TransactionId transactionId)
        {
            var transactionModel = await this.FindTransactionModelAsync(transactionId);

            return Mapper.Map<ITransaction?>(transactionModel);
        }
    }
}
