// Filename: NotificationProviderTest.cs
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

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Notifications.Domain.Tests.AggregateModels.NotificationAggregate
{
    [TestClass]
    public sealed class NotificationProviderTest
    {
        private readonly NotificationProvider _provider = NotificationProvider.Instance;

        [DataRow(NotificationNames.UserEmailUpdated)]
        [DataRow(NotificationNames.ChallengeClosed)]
        [DataRow(NotificationNames.ChallengePublished)]
        [DataRow(NotificationNames.ChallengeParticipantPaid)]
        [DataRow(NotificationNames.ChallengeParticipantRegistered)]
        [DataTestMethod]
        public void FindDescriptionByName_ValidArgument_ShouldBe(string name)
        {
            // Act
            var action = new Action(() => _provider.FindDescriptionByName(name));

            // Assert
            action.Should().NotThrow<InvalidOperationException>();
        }

        [DataRow(null)]
        [DataTestMethod]
        public void FindDescriptionByName_InvalidArgument_ShouldThrowInvalidOperationException(string name)
        {
            // Act
            var action = new Action(() => _provider.FindDescriptionByName(name));

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }
    }
}