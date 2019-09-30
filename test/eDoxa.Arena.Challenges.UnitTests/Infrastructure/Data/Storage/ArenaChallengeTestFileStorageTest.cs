// Filename: ArenaChallengeTestFileStorageTest.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.TestHelpers;
using eDoxa.Arena.Challenges.TestHelpers.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Data.Storage
{
    public sealed class ArenaChallengeTestFileStorageTest : UnitTest
    {
        public ArenaChallengeTestFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void GetChallengesAsync_WithFortyRecords_ShouldHaveCountOfForty()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var challenges = storage.GetChallenges();

            // Assert
            challenges.Should().HaveCount(40);
        }

        [Fact]
        public void GetUsersAsync_WithAdmin_ShouldContainAdminId()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var users = storage.GetUsers();

            // Assert
            users.Should().Contain(user => user.Id == UserId.FromGuid(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));
        }

        [Fact]
        public void GetUsersAsync_WithThousandRecords_ShouldHaveCountOfThousand()
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
