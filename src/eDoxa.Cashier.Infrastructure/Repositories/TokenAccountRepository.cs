// Filename: TokenAccountRepository.cs
// Date Created: 2019-05-02
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
using eDoxa.Cashier.Domain.AggregateModels.TokenAccountAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class TokenAccountRepository
    {
        private readonly CashierDbContext _context;

        public TokenAccountRepository(CashierDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }

    public sealed partial class TokenAccountRepository : ITokenAccountRepository
    {
        public async Task<TokenAccount> FindUserAccountAsync(UserId userId)
        {
            return await _context.TokenAccounts
                .Include(account => account.Transactions)
                .Where(account => account.UserId == userId)
                .SingleOrDefaultAsync();
        }
    }
}