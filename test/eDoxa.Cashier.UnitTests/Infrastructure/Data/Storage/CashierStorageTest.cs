// Filename: CashierStorageTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Infrastructure.Data.Storage;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class CashierStorageTest
    {
        [TestMethod]
        public void TestChallenges_ShouldHaveCountOfFortyRecords()
        {
            // Arrange
            const int recordCount = 40;

            // Act
            var testChallenges = CashierStorage.TestChallenges;

            // Assert
            testChallenges.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestUserIds_ShouldHaveCountOfThousandRecords()
        {
            // Arrange
            const int recordCount = 1000;

            // Act
            var testUsers = CashierStorage.TestUsers;

            // Assert
            testUsers.Should().HaveCount(recordCount);
        }
    }
}
