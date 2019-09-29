// Filename: IdentityTestFileStorageTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Identity.TestHelpers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.UnitTests.Infrastructure.Data.Storage
{
    public sealed class IdentityTestFileStorageTest : UnitTestClass
    {
        public IdentityTestFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetUserClaimsAsync_WithTwoRecords_ShouldHaveCountOfTwo()
        {
            // Arrange
            var storage = TestData.TestFileStorage;

            // Act
            var userClaims = await storage.GetUserClaimsAsync();

            // Assert
            userClaims.Should().HaveCount(2);
        }

        [Fact]
        public async Task GetUserRolesAsync_WithOneRecord_ShouldHaveCountOfOne()
        {
            // Arrange
            var storage = TestData.TestFileStorage;

            // Act
            var userRoles = await storage.GetUserRolesAsync();

            // Assert
            userRoles.Should().HaveCount(1);
        }

        [Fact]
        public async Task GetUsersAsync_WithAdmin_ShouldContainAdminId()
        {
            // Arrange
            var storage = TestData.TestFileStorage;

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().Contain(user => user.Id == Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));
        }

        [Fact]
        public async Task GetUsersAsync_WithThousandRecords_ShouldHaveCountOfThousand()
        {
            // Arrange
            var storage = TestData.TestFileStorage;

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().HaveCount(1000);
        }
    }
}
