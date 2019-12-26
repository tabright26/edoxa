// Filename: UserRepository.cs
// Date Created: 2019-12-17
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using LinqKit;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Notifications.Infrastructure.Repositories
{
    public sealed class UserRepository : IUserRepository
    {
        private readonly NotificationsDbContext _context;

        public UserRepository(NotificationsDbContext context)
        {
            _context = context;
        }

        public IUnitOfWork UnitOfWork => _context;

        public async Task<bool> UserExistsAsync(UserId userId)
        {
            return await _context.Users.AsExpandable().AnyAsync(user => user.Id == userId);
        }

        public async Task<User> FindUserAsync(UserId userId)
        {
            return await _context.Users.AsExpandable().SingleAsync(user => user.Id == userId);
        }

        public void Create(User user)
        {
            _context.Users.Add(user);
        }
    }
}
