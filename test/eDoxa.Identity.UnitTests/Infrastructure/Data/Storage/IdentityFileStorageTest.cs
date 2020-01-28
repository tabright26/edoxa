// Filename: IdentityFileStorageTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.UnitTests.Infrastructure.Data.Storage
{
    public sealed class IdentityFileStorageTest : UnitTest
    {
        public IdentityFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
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
