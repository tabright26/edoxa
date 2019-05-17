// Filename: IMoneyAccountQueries.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain;
using eDoxa.Functional;

namespace eDoxa.Cashier.DTO.Queries
{
    public interface IAccountQueries
    {
        Task<Option<AccountDTO>> GetAccountAsync(AccountCurrency currency);
    }
}