// Filename: NotificationAggregateFactory.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Seedwork.Domain.Factories;

namespace eDoxa.Notifications.Domain.Factories
{
    internal sealed partial class NotificationAggregateFactory : AggregateFactory
    {
        private static readonly Lazy<NotificationAggregateFactory> Lazy = new Lazy<NotificationAggregateFactory>(() => new NotificationAggregateFactory());

        public static NotificationAggregateFactory Instance
        {
            get
            {
                return Lazy.Value;
            }
        }
    }

    internal sealed partial class NotificationAggregateFactory
    {
        public Notification CreateNotification(User user, string title, string message, string redirectUrl = null)
        {
            return new Notification(user, title, message, redirectUrl);
        }

        public void CreateUserNotifications(User user, int count = 1)
        {
            for (var index = 0; index < count; index++)
            {
                user.Notify("Title", "Message", "RedirectUrl");
            }
        }
    }
}