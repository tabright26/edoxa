// Filename: RoleFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Identity.Api.Application.Data.Fakers;
using eDoxa.Seedwork.Common.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Application.Data.Fakers
{
    [TestClass]
    public sealed class RoleFakerTest
    {
        [TestMethod]
        public void FakeRoles_ShouldNotThrow()
        {
            // Arrange
            var roleFaker = new RoleFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var roles = roleFaker.FakeRoles();

                    Console.WriteLine(roles.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeAdminRole_ShouldNotThrow()
        {
            // Arrange
            var roleFaker = new RoleFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var role = roleFaker.FakeAdminRole();

                    Console.WriteLine(role.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }

        [TestMethod]
        public void FakeChallengerRole_ShouldNotThrow()
        {
            // Arrange
            var roleFaker = new RoleFaker();

            // Act
            var action = new Action(
                () =>
                {
                    var role = roleFaker.FakeChallengerRole();

                    Console.WriteLine(role.DumbAsJson());
                }
            );

            // Assert
            action.Should().NotThrow();
        }
    }
}
