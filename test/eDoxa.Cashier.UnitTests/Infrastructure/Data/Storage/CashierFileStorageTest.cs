﻿// Filename: CashierFileStorageTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Cashier.UnitTests.TestHelpers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Storage
{
    public sealed class CashierFileStorageTest : UnitTest
    {
        public CashierFileStorageTest(CashierFakerFixture faker) : base(faker)
        {
        }

        [Fact]
        public async Task GetChallengePayoutStructuresAsync_WithFiftySixRecords_ShouldHaveCountOfFiftySix()
        {
            // Arrange
            var storage = Faker.FileStorage;

            // Act
            var payoutStructures = await storage.GetChallengePayoutStructuresAsync();

            // Assert
            payoutStructures.SelectMany(payoutStructure => payoutStructure).Should().HaveCount(56);
        }
    }
}
