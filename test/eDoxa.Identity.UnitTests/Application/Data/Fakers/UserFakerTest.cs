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

using eDoxa.Identity.Domain.Fakers;
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public class UserFakerTest
    {
        [TestMethod]
        public void FakeNewUsers_ShouldNotThrow()
        {
            // Arrange
            var userFaker = new UserFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var users = userFaker.FakeNewUsers(99);

                    Console.WriteLine(users.DumbAsJson());

                    users.Should().HaveCount(99);
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeNewUser_ShouldNotThrow()
        {
            // Arrange
            var userFaker = new UserFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var user = userFaker.FakeNewUser();

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
