// Filename: UserRepository.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class UserRepository
    {
        private readonly CashierDbContext _context;

        public UserRepository(CashierDbContext context)
        {
            _context = context ?? throw new ArgumentNullException(nameof(context));
        }

        public IUnitOfWork UnitOfWork
        {
            get
            {
                return _context;
            }
        }
    }

    public sealed partial class UserRepository : IUserRepository
    {
        public void Create(User user)
        {
            _context.Users.Add(user);
        }

        public async Task<User> FindAsync(UserId userId)
        {
            return await _context.Users.Include(user => user.Account).Where(user => user.Id == userId).SingleOrDefaultAsync();
        }

        public async Task<User> FindAsNoTrackingAsync(UserId userId)
        {
            return await _context.Users.AsNoTracking().Include(user => user.Account).Where(user => user.Id == userId).SingleOrDefaultAsync();
        }
    }
}