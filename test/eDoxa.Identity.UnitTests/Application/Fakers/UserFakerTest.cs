// Filename: UserFakerTest.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Identity.Api.Infrastructure.Data.Fakers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Application.Fakers
{
    [TestClass]
    public class UserFakerTest
    {
        [TestMethod]
        public void FakeTestUsers_ShouldHaveCountOfThousand()
        {
            // Arrange
            var userFaker = new UserFaker();
            userFaker.UseSeed(1);

            // Act
            var users = userFaker.FakeTestUsers().ToList();

            // Assert
            users.Select(user => user.Id).Distinct().Should().HaveCount(1000);
            users.Select(user => user.Gamertag).Distinct().Should().HaveCount(1000);
        }

        [TestMethod]
        public void FakeTestUsers_ShouldNotThrow()
        {
            // Arrange
            var userFaker = new UserFaker();
            userFaker.UseSeed(1);

            // Act
            var users = userFaker.FakeTestUsers(99).ToList();

            // Assert
            users.Should().HaveCount(99);
        }

        [TestMethod]
        public void FakeTestUser_ShouldNotThrow()
        {
            // Arrange
            var userFaker = new UserFaker();
            userFaker.UseSeed(1);

            // Act
            var user = userFaker.FakeTestUser();

            // Assert
            user.Should().NotBeNull();
        }

        [TestMethod]
        public void FakeAdminUser_ShouldNotThrow()
        {
            // Arrange
            var userFaker = new UserFaker();
            userFaker.UseSeed(1);

            // Act
            var user = userFaker.FakeAdminUser();

            // Assert
            user.Should().NotBeNull();
        }
    }
}
