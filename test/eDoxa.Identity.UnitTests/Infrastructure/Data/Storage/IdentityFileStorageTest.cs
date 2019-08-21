// Filename: IdentityFileStorageTest.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Api.Infrastructure.Data.Storage;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Identity.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class IdentityFileStorageTest
    {
        [TestMethod]
        public async Task GetRolesAsync_WithOneRecord_ShouldHaveCountOfOne()
        {
            // Arrange
            var storage = new IdentityFileStorage();

            // Act
            var roles = await storage.GetRolesAsync();

            // Assert
            roles.Should().HaveCount(1);
        }

        [TestMethod]
        public async Task GetRoleClaimsAsync_WithOneRecord_ShouldHaveCountOfOne()
        {
            // Arrange
            var storage = new IdentityFileStorage();

            // Act
            var roleClaims = await storage.GetRoleClaimsAsync();

            // Assert
            roleClaims.Should().HaveCount(1);
        }
    }
}
