// Filename: INotificationRepository.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Threading.Tasks;

using eDoxa.Notifications.Domain.AggregateModels;
using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Notifications.Domain.Repositories
{
    public interface INotificationRepository : IRepository<Notification>
    {
        void Delete(Notification notification);

        Task<Notification> FindAsync(NotificationId notificationId);
    }
}