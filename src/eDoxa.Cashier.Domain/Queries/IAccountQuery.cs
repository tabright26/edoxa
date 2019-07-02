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

using AutoMapper;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Domain.Queries
{
    public interface IAccountQuery
    {
        IMapper Mapper { get; }

        [ItemCanBeNull]
        Task<IAccount> FindUserAccountAsync(UserId userId);

        [ItemCanBeNull]
        Task<IAccount> FindUserAccountAsync();

        [ItemCanBeNull]
        Task<Balance> FindUserBalanceAsync(UserId userId, CurrencyType currency);

        [ItemCanBeNull]
        Task<Balance> FindUserBalanceAsync(CurrencyType currency);
    }
}
