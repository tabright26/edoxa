// Filename: AccountQuery.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Cashier.Api.Extensions;
using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Queries;
using eDoxa.Cashier.Infrastructure;
using eDoxa.Cashier.Infrastructure.Models;

using JetBrains.Annotations;

using LinqKit;

using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Api.Infrastructure.Queries
{
    public sealed partial class AccountQuery
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public AccountQuery(CashierDbContext cashierDbContext, IMapper mapper, IHttpContextAccessor httpContextAccessor)
        {
            Mapper = mapper;
            _httpContextAccessor = httpContextAccessor;
            Accounts = cashierDbContext.Accounts.AsNoTracking();
        }

        public IMapper Mapper { get; }

        private IQueryable<AccountModel> Accounts { get; }

        [ItemCanBeNull]
        private async Task<AccountModel> FindUserAccountModelAsync(Guid userId)
        {
            var accounts = from account in Accounts.Include(account => account.Transactions).AsExpandable()
                           where account.UserId == userId
                           select account;

            return await accounts.SingleOrDefaultAsync();
        }
    }

    public sealed partial class AccountQuery : IAccountQuery
    {
        [ItemCanBeNull]
        public async Task<IAccount> FindUserAccountAsync(UserId userId)
        {
            var accountModel = await this.FindUserAccountModelAsync(userId);

            return Mapper.Map<IAccount>(accountModel);
        }

        [ItemCanBeNull]
        public async Task<IAccount> FindUserAccountAsync()
        {
            var userId = _httpContextAccessor.GetUserId();

            return await this.FindUserAccountAsync(userId);
        }

        [ItemCanBeNull]
        public async Task<Balance> FindUserBalanceAsync(UserId userId, Currency currency)
        {
            var account = await this.FindUserAccountAsync(userId);

            return account?.GetBalanceFor(currency);
        }

        [ItemCanBeNull]
        public async Task<Balance> FindUserBalanceAsync(Currency currency)
        {
            var userId = _httpContextAccessor.GetUserId();

            return await this.FindUserBalanceAsync(userId, currency);
        }
    }
}
