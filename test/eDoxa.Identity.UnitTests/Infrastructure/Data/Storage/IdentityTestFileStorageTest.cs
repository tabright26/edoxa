// Filename: IdentityTestFileStorageTest.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Data.Storage;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class IdentityTestFileStorageTest
    {
        [TestMethod]
        public async Task GetUserRolesAsync_WithOneRecord_ShouldHaveCountOfOne()
        {
            // Arrange
            var storage = new IdentityTestFileStorage();

            // Act
            var userRoles = await storage.GetUserRolesAsync();

            // Assert
            userRoles.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task GetUserClaimsAsync_WithTwoRecords_ShouldHaveCountOfTwo()
        {
            // Arrange
            var storage = new IdentityTestFileStorage();

            // Act
            var userClaims = await storage.GetUserClaimsAsync();

            // Assert
            userClaims.Should().HaveCount(2);
        }

        [TestMethod]
        public async Task GetUsersAsync_WithThousandRecords_ShouldHaveCountOfThousand()
        {
            // Arrange
            var storage = new IdentityTestFileStorage();

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().HaveCount(1000);
        }

        [TestMethod]
        public async Task GetUsersAsync_WithAdmin_ShouldContainAdminId()
        {
            // Arrange
            var storage = new IdentityTestFileStorage();

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().Contain(user => user.Id == Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));
        }
    }
}
