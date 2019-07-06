// Filename: RoleFakerTest.cs
// Date Created: 2019-06-10
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Api.Application.Fakers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Application.Fakers
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
            var roles = roleFaker.FakeRoles();

            // Assert
            roles.Should().NotBeNull();
        }

        [TestMethod]
        public void FakeAdminRole_ShouldNotThrow()
        {
            // Arrange
            var roleFaker = new RoleFaker();

            // Act
            var role = roleFaker.FakeAdminRole();

            // Assert
            role.Should().NotBeNull();
        }

        [TestMethod]
        public void FakeChallengerRole_ShouldNotThrow()
        {
            // Arrange
            var roleFaker = new RoleFaker();

            // Act
            var role = roleFaker.FakeChallengerRole();

            // Assert
            role.Should().NotBeNull();
        }
    }
}
