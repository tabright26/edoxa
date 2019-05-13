// Filename: MoneyAccountRepository.cs
// Date Created: 2019-05-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.MoneyAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain;

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
        public void Create(MoneyAccount account)
        {
            _context.MoneyAccounts.Add(account);
        }

        public async Task<MoneyAccount> FindUserAccountAsync(UserId userId)
        {
            return await _context.MoneyAccounts
                .Include(account => account.Transactions)
                .Where(account => account.UserId == userId)
                .SingleOrDefaultAsync();
        }
    }
}