// Filename: NotificationTest.cs
// Date Created: 2019-04-06
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Notifications.Domain.AggregateModels.NotificationAggregate;
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
            // Act
            var notification = _notificationAggregateFactory.CreateNotification(
                _userAggregateFactory.CreateUser(),
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
            notification.Should().NotBeNull();
            notification.Id.Should().NotBeNull();
            notification.Timestamp.Should().BeBefore(DateTime.UtcNow);
            notification.Title.Should().NotBeNull();
            notification.Message.Should().NotBeNull();
            notification.IsRead.Should().BeFalse();
            notification.RedirectUrl.Should().BeNull();
            notification.Metadata.Should().NotBeNullOrEmpty();
            notification.User.Should().NotBeNull();
        }

        [DataRow(
            new[]
            {
                "value1", "value2"
            },
            NotificationNames.ChallengeParticipantRegistered
        )]
        [DataTestMethod]
        public void Constructor_ShouldNotThrowException(string[] arguments, string name)
        {
            // Act
            var action = new Action(
                () => _notificationAggregateFactory.CreateNotification(
                    _userAggregateFactory.CreateUser(),
                    name,
                    null,
                    _notificationAggregateFactory.CreateMetadata(arguments)
                )
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void Constructor_User_ShouldThrowArgumentNullException()
        {
            // Act
            var action = new Action(() => _notificationAggregateFactory.CreateNotification(null, NotificationNames.ChallengeParticipantRegistered, null, null));

            // Assert
            action.Should().Throw<ArgumentNullException>();
        }

        [DataRow(
            new string[]
            {
            }
        )]
        [DataTestMethod]
        public void Constructor_Metadata_ShouldThrowArgumentException(string[] arguments)
        {
            // Act
            var action = new Action(
                () => _notificationAggregateFactory.CreateNotification(
                    _userAggregateFactory.CreateUser(),
                    NotificationNames.ChallengeParticipantRegistered,
                    null,
                    _notificationAggregateFactory.CreateMetadata(arguments)
                )
            );

            // Assert
            action.Should().Throw<ArgumentException>();
        }
    }
}