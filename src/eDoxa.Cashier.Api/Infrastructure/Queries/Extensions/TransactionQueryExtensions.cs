// Filename: TransactionQueryExtensions.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;

namespace eDoxa.Cashier.Api.Infrastructure.Queries.Extensions
{
    public static class TransactionQueryExtensions
    {
        public static async Task<IReadOnlyCollection<TransactionModel>> FindUserTransactionModelsAsync(
            this ITransactionQuery transactionQuery,
            UserId userId,
            CurrencyType currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            var transactions = await transactionQuery.FindUserTransactionsAsync(userId, currency, type, status);

            return transactionQuery.Mapper.Map<IReadOnlyCollection<TransactionModel>>(transactions);
        }

        public static async Task<IReadOnlyCollection<TransactionModel>> FindUserTransactionModelsAsync(
            this ITransactionQuery transactionQuery,
            CurrencyType currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            var transactions = await transactionQuery.FindUserTransactionsAsync(currency, type, status);

            return transactionQuery.Mapper.Map<IReadOnlyCollection<TransactionModel>>(transactions);
        }

        public static async Task<IReadOnlyCollection<TransactionViewModel>> FindUserTransactionViewModelsAsync(
            this ITransactionQuery transactionQuery,
            UserId userId,
            CurrencyType currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            var transactions = await transactionQuery.FindUserTransactionsAsync(userId, currency, type, status);

            return transactionQuery.Mapper.Map<IReadOnlyCollection<TransactionViewModel>>(transactions);
        }

        public static async Task<IReadOnlyCollection<TransactionViewModel>> FindUserTransactionViewModelsAsync(
            this ITransactionQuery transactionQuery,
            CurrencyType currency = null,
            TransactionType type = null,
            TransactionStatus status = null
        )
        {
            var transactions = await transactionQuery.FindUserTransactionsAsync(currency, type, status);

            return transactionQuery.Mapper.Map<IReadOnlyCollection<TransactionViewModel>>(transactions);
        }
    }
}
