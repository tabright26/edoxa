// Filename: UserTest.cs
// Date Created: 2019-05-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.Domain.Tests.AggregateModels.UserAggregate
{
    [TestClass]
    public sealed class UserTest
    {
        //private User _user;

        //[TestInitialize]
        //public void TestInitialize()
        //{
        //    _user = new User("francis@edoxa.gg", new PersonalName("Francis", "Quenneville"), new BirthDate(1995, 5, 6), "Admin");
        //    _user.CurrentStatus.Should().Be(UserStatus.Unknown);
        //    _user.PreviousStatus.Should().Be(UserStatus.Unknown);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);
        //}

        //[TestMethod]
        //public void Connect_CurrentStatusUnknownAndPreviousStatusUnknown_ShouldBeCurrentStatusOnlineAndPreviousStatusUnknown()
        //{
        //    // Act
        //    _user.Connect();

        //    // Assert
        //    _user.CurrentStatus.Should().Be(UserStatus.Online);
        //    _user.PreviousStatus.Should().Be(UserStatus.Unknown);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);
        //}

        //[TestMethod]
        //public void Connect_CurrentStatusOfflineAndPreviousStatusInvisible_ShouldBeCurrentStatusInvisibleAndPreviousStatusUnknown()
        //{
        //    // Arrange
        //    const UserStatus currentStatus = UserStatus.Invisible;
        //    _user.ChangeStatus(currentStatus);
        //    _user.Disconnect();
        //    _user.CurrentStatus.Should().Be(UserStatus.Offline);
        //    _user.PreviousStatus.Should().Be(currentStatus);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);

        //    // Act
        //    _user.Connect();

        //    // Assert
        //    _user.CurrentStatus.Should().Be(currentStatus);
        //    _user.PreviousStatus.Should().Be(UserStatus.Unknown);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);
        //}

        //[TestMethod]
        //public void Connect_CurrentStatusOfflineAndPreviousStatusAbsent_ShouldBeCurrentStatusAbsentAndPreviousStatusUnknown()
        //{
        //    // Arrange
        //    const UserStatus currentStatus = UserStatus.Absent;
        //    _user.ChangeStatus(currentStatus);
        //    _user.Disconnect();
        //    _user.CurrentStatus.Should().Be(UserStatus.Offline);
        //    _user.PreviousStatus.Should().Be(currentStatus);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);

        //    // Act
        //    _user.Connect();

        //    // Assert
        //    _user.CurrentStatus.Should().Be(currentStatus);
        //    _user.PreviousStatus.Should().Be(UserStatus.Unknown);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);
        //}

        //[TestMethod]
        //public void Connect_CurrentStatusOfflineAndPreviousStatusOnline_ShouldBeCurrentStatusOnlineAndPreviousStatusUnknown()
        //{
        //    // Arrange
        //    const UserStatus currentStatus = UserStatus.Online;
        //    _user.ChangeStatus(currentStatus);
        //    _user.Disconnect();
        //    _user.CurrentStatus.Should().Be(UserStatus.Offline);
        //    _user.PreviousStatus.Should().Be(currentStatus);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);

        //    // Act
        //    _user.Connect();

        //    // Assert
        //    _user.CurrentStatus.Should().Be(currentStatus);
        //    _user.PreviousStatus.Should().Be(UserStatus.Unknown);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);
        //}

        //[TestMethod]
        //public void Disconnect_CurrentStatusUnknownAndPreviousStatusUnknown_ShouldBeCurrentStatusOfflineAndPreviousStatusUnknown()
        //{
        //    // Act
        //    _user.Disconnect();

        //    // Assert
        //    _user.CurrentStatus.Should().Be(UserStatus.Offline);
        //    _user.PreviousStatus.Should().Be(UserStatus.Unknown);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);
        //}

        //[TestMethod]
        //public void Disconnect_CurrentStatusInvisibleAndPreviousStatusUnknown_ShouldBeCurrentStatusOfflineAndPreviousStatusInvisible()
        //{
        //    // Arrange
        //    const UserStatus currentStatus = UserStatus.Invisible;
        //    _user.ChangeStatus(currentStatus);
        //    _user.CurrentStatus.Should().Be(currentStatus);
        //    _user.PreviousStatus.Should().Be(UserStatus.Unknown);

        //    // Act
        //    _user.Disconnect();

        //    // Assert
        //    _user.CurrentStatus.Should().Be(UserStatus.Offline);
        //    _user.PreviousStatus.Should().Be(currentStatus);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);
        //}

        //[TestMethod]
        //public void Disconnect_CurrentStatusAbsentAndPreviousStatusUnknown_ShouldBeCurrentStatusOfflineAndPreviousStatusAbsent()
        //{
        //    // Arrange
        //    const UserStatus currentStatus = UserStatus.Absent;
        //    _user.ChangeStatus(currentStatus);
        //    _user.CurrentStatus.Should().Be(currentStatus);
        //    _user.PreviousStatus.Should().Be(UserStatus.Unknown);

        //    // Act
        //    _user.Disconnect();

        //    // Assert
        //    _user.CurrentStatus.Should().Be(UserStatus.Offline);
        //    _user.PreviousStatus.Should().Be(currentStatus);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);
        //}

        //[TestMethod]
        //public void Disconnect_CurrentStatusOnlineAndPreviousStatusUnknown_ShouldBeCurrentStatusOfflineAndPreviousStatusOnline()
        //{
        //    // Arrange
        //    const UserStatus currentStatus = UserStatus.Online;
        //    _user.ChangeStatus(currentStatus);
        //    _user.CurrentStatus.Should().Be(currentStatus);
        //    _user.PreviousStatus.Should().Be(UserStatus.Unknown);

        //    // Act
        //    _user.Disconnect();

        //    // Assert
        //    _user.CurrentStatus.Should().Be(UserStatus.Offline);
        //    _user.PreviousStatus.Should().Be(currentStatus);
        //    _user.StatusChanged.Should().BeCloseTo(DateTime.UtcNow, 2000);
        //}
    }
}