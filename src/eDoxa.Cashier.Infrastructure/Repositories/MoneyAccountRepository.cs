// Filename: MoneyAccountRepository.cs
// Date Created: 2019-05-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Common;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class MoneyAccountRepository
    {
        private readonly CashierDbContext _context;

        public MoneyAccountRepository(CashierDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }

    public sealed partial class MoneyAccountRepository : IMoneyAccountRepository
    {
        public async Task<MoneyAccount> GetUserAccountAsync(UserId userId)
        {
            return await _context.MoneyAccounts.Include(account => account.User)
                                 .Include(account => account.Transactions)
                                 .Where(account => account.User.Id == userId)
                                 .SingleOrDefaultAsync();
        }

        public async Task<MoneyAccount> GetMoneyAccountAsNoTrackingAsync(UserId userId)
        {
            return await _context.MoneyAccounts.AsNoTracking()
                .Include(transaction => transaction.Transactions)
                .Where(account => account.User.Id == userId)
                .SingleOrDefaultAsync();
        }
    }
}
