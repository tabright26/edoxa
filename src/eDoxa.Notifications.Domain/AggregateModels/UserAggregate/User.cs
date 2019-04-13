// Filename: User.cs
// Date Created: 2019-03-26
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Collections.ObjectModel;

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Common;

namespace eDoxa.Notifications.Domain.AggregateModels.UserAggregate
{
    public class User : Entity<UserId>, IAggregateRoot
    {
        private Collection<Notification> _notifications;

        private User(UserId userId) : this()
        {
            Id = userId;
        }

        private User()
        {
            _notifications = new Collection<Notification>();
        }

        public IReadOnlyCollection<Notification> Notifications
        {
            get
            {
                return _notifications;
            }
        }

        public static User Create(UserData data)
        {
            return new User(UserId.FromGuid(data.Id));
        }

        public static User Create(UserId userId)
        {
            return new User(userId);
        }

        public Notification Notify(string title, string message, string redirectUrl = null)
        {
            var notification = new Notification(this, title, message, redirectUrl);

            _notifications.Add(notification);

            return notification;
        }
    }
}