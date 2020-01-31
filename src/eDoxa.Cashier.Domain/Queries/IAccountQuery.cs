// Filename: IAccountQuery.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface IAccountQuery
    {
        Task<IAccount?> FindUserAccountAsync(UserId userId);

        Task<Balance?> FindUserBalanceAsync(UserId userId, CurrencyType currencyType);
    }
}
