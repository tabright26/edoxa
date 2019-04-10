// Filename: NotificationsAssert.cs
// Date Created: 2019-04-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;

using FluentAssertions;

namespace eDoxa.Notifications.Infrastructure.Tests.Asserts
{
    internal static class NotificationsAssert
    {
        public static void IsMapped(User user)
        {
            user.Should().NotBeNull();

            user.Id.Should().NotBeNull();

            IsMapped(user.Notifications);
        }

        public static void IsMapped(IReadOnlyCollection<Notification> notifications)
        {
            notifications.Should().NotBeNull();

            foreach (var notification in notifications)
            {
                IsMapped(notification);
            }
        }

        public static void IsMapped(Notification notification)
        {
            notification.Should().NotBeNull();

            notification.Id.Should().NotBeNull();

            notification.Timestamp.Should().BeBefore(DateTime.UtcNow);

            notification.Title.Should().NotBeNull();

            notification.Message.Should().NotBeNull();

            notification.IsRead.Should().BeFalse();

            notification.User.Should().NotBeNull();
        }
    }
}