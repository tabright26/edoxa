// Filename: UserFakerTest.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Linq;

using eDoxa.Identity.Api.Application.Fakers;
using eDoxa.Seedwork.Common.Extensions;

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
            users.Select(user => user.UserName).Distinct().Should().HaveCount(1000);
        }

        [TestMethod]
        public void FakeTestUsers_ShouldNotThrow()
        {
            // Arrange
            var userFaker = new UserFaker();
            userFaker.UseSeed(1);

            // Act
            var action = new Action(
                () =>
                {
                    var users = userFaker.FakeTestUsers(99).ToList();

                    Console.WriteLine(users.DumbAsJson());

                    users.Should().HaveCount(99);
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeTestUser_ShouldNotThrow()
        {
            // Arrange
            var userFaker = new UserFaker();
            userFaker.UseSeed(1);

            // Act
            var action = new Action(
                () =>
                {
                    var user = userFaker.FakeTestUser();

                    Console.WriteLine(user.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeAdminUser_ShouldNotThrow()
        {
            // Arrange
            var userFaker = new UserFaker();
            userFaker.UseSeed(1);

            // Act
            var action = new Action(
                () =>
                {
                    var user = userFaker.FakeAdminUser();

                    Console.WriteLine(user.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }
    }
}
