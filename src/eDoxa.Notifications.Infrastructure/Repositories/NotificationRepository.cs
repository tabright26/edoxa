// Filename: NotificationRepository.cs
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
using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Notifications.Domain.Repositories;
using eDoxa.Seedwork.Domain;

using Microsoft.EntityFrameworkCore;

namespace eDoxa.Notifications.Infrastructure.Repositories
{
    public sealed partial class NotificationRepository
    {
        private readonly NotificationsDbContext _context;

        public NotificationRepository(NotificationsDbContext context)
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

    public sealed partial class NotificationRepository : INotificationRepository
    {
        public void Delete(Notification notification)
        {
            _context.Notifications.Remove(notification ?? throw new ArgumentNullException());
        }

        public async Task<Notification> FindAsync(NotificationId notificationId)
        {
            return await _context.Notifications.Include(notification => notification.User)
                                 .Where(notification => notification.Id == notificationId)
                                 .SingleOrDefaultAsync();
        }
    }
}