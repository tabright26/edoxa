// Filename: CashierFileStorageTest.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Storage
{
    public sealed class CashierFileStorageTest : UnitTest
    {
        public CashierFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void GetChallengePayouts_WithFiftySixRecords_ShouldHaveCountOfFiftySix()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var payouts = storage.GetChallengePayouts();

            // Assert
            payouts.SelectMany(payout => payout).Should().HaveCount(55);
        }
    }
}
