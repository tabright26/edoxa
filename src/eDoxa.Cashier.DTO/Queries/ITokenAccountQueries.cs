// Filename: ITokenAccountQueries.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Functional;

namespace eDoxa.Cashier.DTO.Queries
{
    public interface ITokenAccountQueries
    {
        Task<Option<TokenAccountDTO>> FindAccountAsync(UserId userId);

        Task<Option<TokenTransactionListDTO>> FindTransactionsAsync(UserId userId);
    }
}