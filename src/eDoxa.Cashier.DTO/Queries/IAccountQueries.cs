// Filename: IAccountQueries.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Functional;
using eDoxa.Seedwork.Domain.Enumerations;

namespace eDoxa.Cashier.DTO.Queries
{
    public interface IAccountQueries
    {
        Task<Option<AccountDTO>> GetAccountAsync(Currency currency);
    }
}
