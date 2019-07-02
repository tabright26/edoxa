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

using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Domain.ViewModels;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.ValueObjects;

using JetBrains.Annotations;

namespace eDoxa.Cashier.Api.Infrastructure.Queries.Extensions
{
    public static class AccountQueryExtensions
    {
        [ItemCanBeNull]
        public static async Task<AccountModel> FindUserAccountModelAsync(this IAccountQuery accountQuery, UserId userId)
        {
            var account = await accountQuery.FindUserAccountAsync(userId);

            return accountQuery.Mapper.Map<AccountModel>(account);
        }

        [ItemCanBeNull]
        public static async Task<AccountModel> FindUserAccountModelAsync(this IAccountQuery accountQuery)
        {
            var account = await accountQuery.FindUserAccountAsync();

            return accountQuery.Mapper.Map<AccountModel>(account);
        }

        [ItemCanBeNull]
        public static async Task<BalanceViewModel> FindUserBalanceViewModelAsync(this IAccountQuery accountQuery, UserId userId, CurrencyType currency)
        {
            var balance = await accountQuery.FindUserBalanceAsync(userId, currency);

            return accountQuery.Mapper.Map<BalanceViewModel>(balance);
        }

        [ItemCanBeNull]
        public static async Task<BalanceViewModel> FindUserBalanceViewModelAsync(this IAccountQuery accountQuery, CurrencyType currency)
        {
            var balance = await accountQuery.FindUserBalanceAsync(currency);

            return accountQuery.Mapper.Map<BalanceViewModel>(balance);
        }
    }
}
