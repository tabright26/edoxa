// Filename: ITransactionQueries.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Common.Enumerations;

namespace eDoxa.Cashier.DTO.Queries
{
    public interface ITransactionQueries
    {
        Task<TransactionListDTO> GetTransactionsAsync(CurrencyType currency = null, TransactionType type = null, TransactionStatus status = null);
    }
}
