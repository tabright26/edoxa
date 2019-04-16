// Filename: NotificationQueries.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using AutoMapper;

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Notifications.DTO;
using eDoxa.Notifications.DTO.Queries;
using eDoxa.Notifications.Infrastructure;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore;

namespace eDoxa.Notifications.Application.Queries
{
    public sealed partial class NotificationQueries
    {
        private readonly NotificationsDbContext _context;
        private readonly IMapper _mapper;

        public NotificationQueries(NotificationsDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        private async Task<IEnumerable<Notification>> FindUserNotificationsAsNoTrackingAsync(UserId userId)
        {
            return await _context.Notifications.AsNoTracking()
                                 .Where(notification => notification.User.Id == userId)
                                 .OrderByDescending(notification => notification.Timestamp)
                                 .ToListAsync();
        }

        private async Task<Notification> FindUserNotificationAsNoTrackingAsync(UserId userId, NotificationId notificationId)
        {
            return await _context.Notifications.AsNoTracking()
                                 .Where(notification => notification.User.Id == userId && notification.Id == notificationId)
                                 .SingleOrDefaultAsync();
        }
    }

    public sealed partial class NotificationQueries : INotificationQueries
    {
        public async Task<NotificationListDTO> FindUserNotificationsAsync(UserId userId)
        {
            var notifications = await this.FindUserNotificationsAsNoTrackingAsync(userId);

            return _mapper.Map<NotificationListDTO>(notifications);
        }

        [ItemCanBeNull]
        public async Task<NotificationDTO> FindUserNotificationAsync(UserId userId, NotificationId notificationId)
        {
            var notification = await this.FindUserNotificationAsNoTrackingAsync(userId, notificationId);

            return _mapper.Map<NotificationDTO>(notification);
        }
    }
}