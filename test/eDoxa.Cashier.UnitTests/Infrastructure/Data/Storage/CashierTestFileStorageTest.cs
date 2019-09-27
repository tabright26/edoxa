// Filename: CashierTestFileStorageTest.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Cashier.Domain.AggregateModels.AccountAggregate;
using eDoxa.Storage.Azure.File;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class CashierTestFileStorageTest
    {
        [TestMethod]
        public async Task GetChallengesAsync_WithFortyRecords_ShouldHaveCountOfForty()
        {
            // Arrange
            var storage = new CashierTestFileStorage(new AzureFileStorage());

            // Act
            var testChallenges = await storage.GetChallengesAsync();

            // Assert
            testChallenges.Should().HaveCount(40);
        }

        [TestMethod]
        public async Task GetUsersAsync_WithThousandRecords_ShouldHaveCountOfThousand()
        {
            // Arrange
            var storage = new CashierTestFileStorage(new AzureFileStorage());

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().HaveCount(1000);
        }

        [TestMethod]
        public async Task GetUsersAsync_ShouldContainAdminId()
        {
            // Arrange
            var storage = new CashierTestFileStorage(new AzureFileStorage());

            // Act
            var users = await storage.GetUsersAsync();

            // Assert
            users.Should().Contain(user => user.Id == UserId.FromGuid(Guid.Parse("e4655fe0-affd-4323-b022-bdb2ebde6091")));
        }
    }
}
