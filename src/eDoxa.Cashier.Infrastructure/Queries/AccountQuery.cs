// Filename: AccountQuery.cs
// Date Created: 2020-01-22
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure.Extensions;
using eDoxa.Cashier.Infrastructure.Models;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Queries
{
    public sealed partial class AccountQuery
    {
        public AccountQuery(CashierDbContext cashierDbContext)
        {
            Accounts = cashierDbContext.Set<AccountModel>().AsNoTracking();
        }

        private IQueryable<AccountModel> Accounts { get; }

        private async Task<AccountModel?> FindUserAccountModelAsync(Guid userId)
        {
            var accounts = from account in Accounts.Include(account => account.Transactions).AsExpandable()
                           where account.Id == userId
                           select account;

            return await accounts.SingleOrDefaultAsync();
        }
    }

    public sealed partial class AccountQuery : IAccountQuery
    {
        public async Task<IAccount?> FindUserAccountAsync(UserId userId)
        {
            var accountModel = await this.FindUserAccountModelAsync(userId);

            return accountModel?.ToEntity();
        }

        public async Task<Balance?> FindUserBalanceAsync(UserId userId, CurrencyType currencyType)
        {
            var account = await this.FindUserAccountAsync(userId);

            return account?.GetBalanceFor(currencyType);
        }
    }
}
