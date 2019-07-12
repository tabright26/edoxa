// Filename: ArenaChallengesStorageTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Arena.Challenges.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class ArenaChallengesStorageTest
    {
        [TestMethod]
        public void TestChallenges_ShouldHaveCountOfFortyRecords()
        {
            // Arrange
            const int recordCount = 40;

            // Act
            var testChallenges = ArenaChallengesStorage.TestChallenges.ToList();

            // Assert
            testChallenges.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestUsers_ShouldHaveCountOfThousandRecords()
        {
            // Arrange
            const int recordCount = 1000;

            // Act
            var testUsers = ArenaChallengesStorage.TestUsers.ToList();

            // Assert
            testUsers.Should().HaveCount(recordCount);
        }
    }
}
