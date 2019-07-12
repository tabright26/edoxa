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
        public void TestUserIds_ShouldHaveCountOfThousandRecords()
        {
            // Arrange
            const int recordCount = 1000;

            // Act
            var testUserIds = ArenaChallengesStorage.TestUserIds.ToList();

            // Assert
            testUserIds.Should().HaveCount(recordCount);
        }
    }
}
