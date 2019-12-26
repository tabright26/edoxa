// Filename: CashierTestFileStorageTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;
using eDoxa.Seedwork.Domain.Misc;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Storage
{
    public sealed class CashierTestFileStorageTest : UnitTest
    {
        public CashierTestFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void GetChallenges_WithFortyRecords_ShouldHaveCountOfForty()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var testChallenges = storage.GetChallenges();

            // Assert
            testChallenges.Should().HaveCount(40);
        }

        [Fact]
        public void GetUsers_ShouldContainAdminId()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var users = storage.GetUsers();

            // Assert
            users.Should().Contain(userId => userId == UserId.FromGuid(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));
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
