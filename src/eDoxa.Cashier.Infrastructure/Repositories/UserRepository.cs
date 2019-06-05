// Filename: UserRepository.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Common;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class UserRepository
    {
        private readonly CashierDbContext _context;

        public UserRepository(CashierDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;
    }

    public sealed partial class UserRepository : IUserRepository
    {
        public void Create(UserId userId, string connectAccountId, string customerId)
        {
            _context.Add(new User(userId, connectAccountId, customerId));
        }

        [ItemCanBeNull]
        public async Task<User> GetUserAsync(UserId userId)
        {
            return await _context.Users.Where(user => user.Id == userId).SingleAsync();
        }

        [ItemCanBeNull]
        public async Task<User> GetUserAsNoTrackingAsync(UserId userId)
        {
            return await _context.Users.AsNoTracking().Where(user => user.Id == userId).SingleAsync();
        }
    }
}
