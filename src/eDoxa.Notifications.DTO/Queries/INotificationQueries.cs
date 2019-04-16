// Filename: INotificationQueries.cs
// Date Created: 2019-04-03
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels;
using JetBrains.Annotations;

namespace eDoxa.Notifications.DTO.Queries
{
    public interface INotificationQueries
    {
        Task<NotificationListDTO> FindUserNotificationsAsync(UserId userId);

        [ItemCanBeNull]
        Task<NotificationDTO> FindUserNotificationAsync(UserId userId, NotificationId notificationId);
    }
}