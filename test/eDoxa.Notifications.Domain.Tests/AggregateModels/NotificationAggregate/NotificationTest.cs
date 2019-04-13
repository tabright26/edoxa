// Filename: NotificationTest.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Notifications.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Domain.Tests.AggregateModels.NotificationAggregate
{
    [TestClass]
    public sealed class NotificationTest
    {
        private readonly NotificationAggregateFactory _notificationAggregateFactory = NotificationAggregateFactory.Instance;
        private readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public void Constructor_ValidArguments_ShouldNotBeNull()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            // Act
            var notification = _notificationAggregateFactory.CreateNotification(user, "Title", "Message", "RedirectUrl");

            // Assert
            notification.Should().NotBeNull();
            notification.Id.Should().NotBeNull();
            notification.Timestamp.Should().BeBefore(DateTime.UtcNow);
            notification.Title.Should().NotBeNull();
            notification.Message.Should().NotBeNull();
            notification.RedirectUrl.Should().NotBeNull();
            notification.IsRead.Should().BeFalse();
            notification.User.Should().NotBeNull();
        }
    }
}