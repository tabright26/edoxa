// Filename: IdentityFileStorageTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.UnitTests.Helpers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.UnitTests.Infrastructure.Data.Storage
{
    public sealed class IdentityFileStorageTest : UnitTest
    {
        public IdentityFileStorageTest(IdentityFakerFixture faker) : base(faker)
        {
        }

        [Fact]
        public async Task GetRoleClaimsAsync_WithOneRecord_ShouldHaveCountOfOne()
        {
            // Arrange
            var storage = Faker.FileStorage;

            // Act
            var roleClaims = await storage.GetRoleClaimsAsync();

            // Assert
            roleClaims.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetRolesAsync_WithOneRecord_ShouldHaveCountOfOne()
        {
            // Arrange
            var storage = Faker.FileStorage;

            // Act
            var roles = await storage.GetRolesAsync();

            // Assert
            roles.Should().HaveCount(1);
        }
    }
}
