// Filename: TransactionQuery.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;
using eDoxa.Seedwork.Common.ValueObjects;

using JetBrains.Annotations;

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
        public IMapper Mapper { get; }

        private IQueryable<TransactionModel> Transactions { get; }

        [ItemCanBeNull]
        private async Task<IReadOnlyCollection<TransactionModel>> FindTransactionModelAsync(
            Guid userId,
            int? currency = null,
            int? type = null,
            int? status = null
        )
        {
            var transactions = from transaction in Transactions.Include(account => account.Account).ThenInclude(account => account.User).AsExpandable()
                               where transaction.Account.User.Id == userId &&
                                     (currency == null || transaction.Currency == currency) &&
                                     (type == null || transaction.Type == type) &&
                                     (status == null || transaction.Status == status)
                               orderby transaction.Timestamp descending
                               select transaction;

            return await transactions.ToListAsync();
        }
    }

    public sealed partial class TransactionQuery : ITransactionQuery
    {
        public async Task<IReadOnlyCollection<ITransaction>> FindUserTransactionsAsync(
            CurrencyType currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            var userId = _httpContextAccessor.GetUserId();

            return await this.FindUserTransactionsAsync(userId, currency, type, status);
        }

        public async Task<IReadOnlyCollection<ITransaction>> FindUserTransactionsAsync(
            UserId userId,
            CurrencyType currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            var transactionModels = await this.FindTransactionModelAsync(userId, currency?.Value, type?.Value, status?.Value);

            return Mapper.Map<IReadOnlyCollection<ITransaction>>(transactionModels);
        }
    }
}
