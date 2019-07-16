// Filename: CashierStorageTest.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class CashierStorageTest
    {
        [TestMethod]
        public void TestChallenges_ShouldHaveRecordCountOfForty()
        {
            // Arrange
            const int recordCount = 40;

            // Act
            var testChallenges = CashierStorage.TestChallenges;

            // Assert
            testChallenges.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestUsers_ShouldHaveRecordCountOfThousand()
        {
            // Arrange
            const int recordCount = 1000;

            // Act
            var testUsers = CashierStorage.TestUsers;

            // Assert
            testUsers.Should().HaveCount(recordCount);
        }

        [TestMethod]
        public void TestAdmin_ShouldBeTestAdminId()
        {
            // Act
            var testAdmin = CashierStorage.TestAdmin;

            // Assert
            testAdmin.Id.Should().Be(UserId.FromGuid(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));
        }
    }
}
