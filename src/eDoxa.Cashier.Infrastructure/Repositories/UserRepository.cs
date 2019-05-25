// Filename: UserRepository.cs
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

using eDoxa.Cashier.Domain.AggregateModels;
using eDoxa.Cashier.Domain.AggregateModels.UserAggregate;
using eDoxa.Cashier.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Entities;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Cashier.Infrastructure.Repositories
{
    public sealed partial class UserRepository
    {
        private readonly CashierDbContext _dbContext;

        public UserRepository(CashierDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IUnitOfWork UnitOfWork => _dbContext;
    }

    public sealed partial class UserRepository : IUserRepository
    {
        public void Create(UserId userId, StripeAccountId accountId, StripeCustomerId customerId)
        {
            _dbContext.Add(new User(userId, accountId, customerId));
        }

        public async Task<User> FindUserAsync(UserId userId)
        {
            return await _dbContext.Users.Where(user => user.Id == userId).SingleAsync();
        }

        public async Task<User> FindUserAsNoTrackingAsync(UserId userId)
        {
            return await _dbContext.Users.AsNoTracking().Where(user => user.Id == userId).SingleAsync();
        }
    }
}
