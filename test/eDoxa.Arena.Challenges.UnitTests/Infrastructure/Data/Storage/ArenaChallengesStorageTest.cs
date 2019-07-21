// Filename: ArenaChallengesStorageTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Arena.Challenges.Domain.AggregateModels;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class ArenaChallengesStorageTest
    {
        [TestMethod]
        public void TestChallenges_ShouldHaveRecordCountOfForty()
        {
            // Arrange
            const int recordCount = 40;

            // Act
            var testChallenges = ArenaChallengesStorage.TestChallenges.ToList();

            // Assert
            testChallenges.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestUsers_ShouldHaveRecordCountOfThousand()
        {
            // Arrange
            const int recordCount = 1000;

            // Act
            var testUsers = ArenaChallengesStorage.TestUsers.ToList();

            // Assert
            testUsers.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestAdmin_ShouldBeTestAdminId()
        {
            // Act
            var testAdmin = ArenaChallengesStorage.TestAdmin;

            // Assert
            testAdmin.Id.Should().Be(UserId.FromGuid(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));
        }
    }
}
