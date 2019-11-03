// Filename: IdentityFileStorageTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.UnitTests.Infrastructure.Data.Storage
{
    public sealed class IdentityFileStorageTest : UnitTest
    {
        public IdentityFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void GetRoleClaims_WithOneRecord_ShouldHaveCountOfOne()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var roleClaims = storage.GetRoleClaims();

            // Assert
            roleClaims.Should().HaveCount(1);
        }

        [Fact]
        public void GetRoles_WithOneRecord_ShouldHaveCountOfOne()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var roles = storage.GetRoles();

            // Assert
            roles.Should().HaveCount(1);
        }
    }
}
