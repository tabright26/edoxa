// Filename: ITransactionQuery.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface ITransactionQuery
    {
        Task<IReadOnlyCollection<ITransaction>> FetchUserTransactionsAsync(
            UserId userId,
            CurrencyType? currency = null,
            TransactionType? type = null,
            TransactionStatus? status = null
        );

        Task<ITransaction?> FindTransactionAsync(TransactionId transactionId);
    }
}
