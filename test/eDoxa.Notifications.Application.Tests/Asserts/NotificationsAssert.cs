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

using eDoxa.Notifications.DTO;

using FluentAssertions;

namespace eDoxa.Notifications.Application.Tests.Asserts
{
    internal static class NotificationsAssert
    {
        public static void IsMapped(NotificationListDTO notifications)
        {
            notifications.Should().NotBeNull();

            foreach (var challenge in notifications)
            {
                IsMapped(challenge);
            }
        }

        public static void IsMapped(NotificationDTO notification)
        {
            notification.Should().NotBeNull();

            notification.Id.Should().NotBeEmpty();

            notification.Timestamp.Should().BeBefore(DateTime.UtcNow);

            notification.Title.Should().NotBeNull();

            notification.Message.Should().NotBeNull();

            notification.IsRead.Should().BeFalse();
        }
    }
}