﻿// Filename: ArenaChallengeTestFileStorageTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Challenges.TestHelper;
using eDoxa.Challenges.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Challenges.UnitTests.Infrastructure.Data.Storage
{
    public sealed class ChallengeTestFileStorageTest : UnitTest
    {
        public ChallengeTestFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator validator) : base(
            testData,
            testMapper,
            validator)
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
