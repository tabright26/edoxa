// Filename: CashierFileStorageTest.cs
// Date Created: 2019-08-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.Api.Infrastructure.Data.Storage;
using eDoxa.Storage.Azure.File;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Storage
{
    [TestClass]
    public sealed class CashierFileStorageTest
    {
        [TestMethod]
        public async Task GetChallengePayoutStructuresAsync_WithFiftySixRecords_ShouldHaveCountOfFiftySix()
        {
            // Arrange
            var storage = new CashierFileStorage(new AzureFileStorage());

            // Act
            var payoutStructures = await storage.GetChallengePayoutStructuresAsync();

            // Assert
            payoutStructures.SelectMany(payoutStructure => payoutStructure).Should().HaveCount(56);
        }
    }
}
