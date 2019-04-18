// Filename: UserTest.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Notifications.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Domain.Tests.AggregateModels.UserAggregate
{
    [TestClass]
    public sealed class UserTest
    {
        private static readonly UserAggregateFactory UserAggregateFactory = UserAggregateFactory.Instance;

        [TestMethod]
        public void Constructor_User_ShouldNotBeNull()
        {
            // Act
            var user = UserAggregateFactory.CreateUser();

            // Assert
            user.Should().NotBeNull();
        }

        [TestMethod]
        public void Notify_Notifications_ShouldOnlyContainNotification()
        {
            // Arrange
            var user = UserAggregateFactory.CreateUser();

            // Act
            var notification = user.Notify(
                "Title",
                "Message",
                "RedirectUrl"
            );

            // Assert
            user.Notifications.Should().OnlyContain(x => x == notification);
        }
    }
}