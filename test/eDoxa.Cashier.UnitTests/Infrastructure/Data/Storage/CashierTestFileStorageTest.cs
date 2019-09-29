// Filename: CashierTestFileStorageTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Cashier.TestHelpers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Storage
{
    public sealed class CashierTestFileStorageTest : UnitTestClass
    {
        public CashierTestFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public async Task GetChallengesAsync_WithFortyRecords_ShouldHaveCountOfForty()
        {
            // Arrange
            var storage = TestData.TestFileStorage;

            // Act
            var testChallenges = await storage.GetChallengesAsync();

            // Assert
            testChallenges.Should().HaveCount(40);
        }

        [Fact]
        public async Task GetUsersAsync_ShouldContainAdminId()
        {
            // Arrange
            var storage = TestData.TestFileStorage;

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().Contain(user => user.Id == UserId.FromGuid(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));
        }

        [Fact]
        public async Task GetUsersAsync_WithThousandRecords_ShouldHaveCountOfThousand()
        {
            // Arrange
            var storage = TestData.TestFileStorage;

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().HaveCount(1000);
        }
    }
}
