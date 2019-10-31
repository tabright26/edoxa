// Filename: IdentityTestFileStorageTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Identity.TestHelper;
using eDoxa.Identity.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Identity.UnitTests.Infrastructure.Data.Storage
{
    public sealed class IdentityTestFileStorageTest : UnitTest
    {
        public IdentityTestFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void GetUserClaims_WithTwoRecords_ShouldHaveCountOfTwo()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var userClaims = storage.GetUserClaims();

            // Assert
            userClaims.Should().HaveCount(2);
        }

        [Fact]
        public void GetUserRoles_WithOneRecord_ShouldHaveCountOfOne()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var userRoles = storage.GetUserRoles();

            // Assert
            userRoles.Should().HaveCount(1);
        }

        [Fact]
        public void GetUsers_WithAdmin_ShouldContainAdminId()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var users = storage.GetUsers();

            // Assert
            users.Should().Contain(user => user.Id == Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091"));
        }

        [Fact]
        public void GetUsers_WithThousandRecords_ShouldHaveCountOfThousand()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var users = storage.GetUsers();

            // Assert
            users.Should().HaveCount(1000);
        }
    }
}
