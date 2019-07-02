// Filename: IBalanceQuery.cs
// Date Created: 2019-07-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Seedwork.Common.Enumerations;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface IBalanceQuery
    {
        [ItemCanBeNull]
        Task<BalanceViewModel> GetBalanceAsync(CurrencyType currencyType);
    }
}
