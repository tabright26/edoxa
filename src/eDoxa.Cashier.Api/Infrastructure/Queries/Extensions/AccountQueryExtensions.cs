// Filename: AccountQueryExtensions.cs
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
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Cashier.Responses;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Cashier.Api.Infrastructure.Queries.Extensions
{
    public static class AccountQueryExtensions
    {
        public static async Task<AccountModel?> FindUserAccountModelAsync(this IAccountQuery accountQuery, UserId userId)
        {
            var account = await accountQuery.FindUserAccountAsync(userId);

            return accountQuery.Mapper.Map<AccountModel>(account);
        }

        public static async Task<AccountModel?> FindUserAccountModelAsync(this IAccountQuery accountQuery)
        {
            var account = await accountQuery.FindUserAccountAsync();

            return accountQuery.Mapper.Map<AccountModel>(account);
        }

        public static async Task<BalanceResponse?> FindUserBalanceResponseAsync(this IAccountQuery accountQuery, UserId userId, Currency currency)
        {
            var balance = await accountQuery.FindUserBalanceAsync(userId, currency);

            return accountQuery.Mapper.Map<BalanceResponse>(balance);
        }

        public static async Task<BalanceResponse?> FindUserBalanceResponseAsync(this IAccountQuery accountQuery, Currency currency)
        {
            var balance = await accountQuery.FindUserBalanceAsync(currency);

            return accountQuery.Mapper.Map<BalanceResponse>(balance);
        }
    }
}
