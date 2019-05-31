// Filename: AccountRepository.cs
// Date Created: 2019-05-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Common;
using eDoxa.Seedwork.Domain.Common.Enumerations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class AccountRepository
    {
        private readonly CashierDbContext _context;

        public AccountRepository(CashierDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }

    public sealed partial class AccountRepository : IAccountRepository
    {
        public async Task<Account> GetAccountAsync(UserId userId)
        {
            return await _context.Accounts.Include(account => account.User)
                .Include(account => account.Transactions)
                .Where(account => account.User.Id == userId)
                .SingleOrDefaultAsync();
        }

        public async Task<Balance> GetBalanceAsNoTrackingAsync(UserId userId, CurrencyType currency)
        {
            var transactions = await this.GetTransactionsAsNoTrackingAsync(userId);

            return new Balance(transactions, currency);
        }

        public async Task<IReadOnlyCollection<Transaction>> GetTransactionsAsNoTrackingAsync(UserId userId)
        {
            var account = await _context.Accounts.AsNoTracking()
                .Include(x => x.User)
                .Include(x => x.Transactions)
                .Where(x => x.User.Id == userId)
                .SingleOrDefaultAsync();

            return account.Transactions;
        }
    }
}
