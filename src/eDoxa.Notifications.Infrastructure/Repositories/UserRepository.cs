// Filename: UserRepository.cs
// Date Created: 2019-04-06
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

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Seedwork.Domain;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Notifications.Infrastructure.Repositories
{
    public sealed partial class UserRepository
    {
        internal static readonly string ExpandNotifications = nameof(User.Notifications);

        private readonly NotificationsDbContext _context;

        public UserRepository(NotificationsDbContext context)
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
            return await _context.Users.Include(ExpandNotifications).Where(user => user.Id == userId).SingleOrDefaultAsync();
        }
    }
}