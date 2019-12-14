// Filename: TransactionQueryExtensions.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Grpc.Protos.Cashier.Dtos;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Infrastructure.Queries.Extensions
{
    public static class TransactionQueryExtensions
    {
        public static async Task<IReadOnlyCollection<TransactionModel>> FetchUserTransactionModelsAsync(
            this ITransactionQuery transactionQuery,
            UserId userId,
            Currency? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        )
        {
            var transactions = await transactionQuery.FetchUserTransactionsAsync(
                userId,
                currency,
                type,
                status);

            return transactionQuery.Mapper.Map<IReadOnlyCollection<TransactionModel>>(transactions);
        }

        public static async Task<IReadOnlyCollection<TransactionModel>> FetchUserTransactionModelsAsync(
            this ITransactionQuery transactionQuery,
            Currency? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        )
        {
            var transactions = await transactionQuery.FetchUserTransactionsAsync(currency, type, status);

            return transactionQuery.Mapper.Map<IReadOnlyCollection<TransactionModel>>(transactions);
        }

        public static async Task<IReadOnlyCollection<TransactionDto>> FetchUserTransactionResponsesAsync(
            this ITransactionQuery transactionQuery,
            UserId userId,
            Currency? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        )
        {
            var transactions = await transactionQuery.FetchUserTransactionsAsync(
                userId,
                currency,
                type,
                status);

            return transactionQuery.Mapper.Map<IReadOnlyCollection<TransactionDto>>(transactions);
        }

        public static async Task<IReadOnlyCollection<TransactionDto>> FetchUserTransactionResponsesAsync(
            this ITransactionQuery transactionQuery,
            Currency? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        )
        {
            var transactions = await transactionQuery.FetchUserTransactionsAsync(currency, type, status);

            return transactionQuery.Mapper.Map<IReadOnlyCollection<TransactionDto>>(transactions);
        }

        public static async Task<TransactionModel?> FindTransactionModelAsync(this ITransactionQuery transactionQuery, TransactionId transactionId)
        {
            var transactions = await transactionQuery.FindTransactionAsync(transactionId);

            return transactionQuery.Mapper.Map<TransactionModel?>(transactions);
        }

        public static async Task<TransactionDto?> FindTransactionResponseAsync(this ITransactionQuery transactionQuery, TransactionId transactionId)
        {
            var transactions = await transactionQuery.FindTransactionAsync(transactionId);

            return transactionQuery.Mapper.Map<TransactionDto?>(transactions);
        }
    }
}
