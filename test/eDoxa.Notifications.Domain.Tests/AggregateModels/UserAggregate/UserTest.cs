// Filename: UserTest.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
using eDoxa.Notifications.Domain.Factories;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Domain.Tests.AggregateModels.UserAggregate
{
    [TestClass]
    public sealed class UserTest
    {
        private static readonly UserAggregateFactory _userAggregateFactory = UserAggregateFactory.Instance;
        private static readonly NotificationAggregateFactory _notificationAggregateFactory = NotificationAggregateFactory.Instance;

        [TestMethod]
        public void Constructor_User_ShouldNotBeNull()
        {
            // Act
            var user = _userAggregateFactory.CreateUser();

            // Assert
            user.Should().NotBeNull();
        }

        [TestMethod]
        public void Notify_Notifications_ShouldOnlyContainNotification()
        {
            // Arrange
            var user = _userAggregateFactory.CreateUser();

            // Act
            var notification = user.Notify(
                NotificationNames.ChallengeParticipantRegistered,
                null,
                _notificationAggregateFactory.CreateMetadata(
                    new[]
                    {
                        "value1", "value2"
                    }
                )
            );

            // Assert
            user.Notifications.Should().OnlyContain(x => x == notification);
        }
    }
}