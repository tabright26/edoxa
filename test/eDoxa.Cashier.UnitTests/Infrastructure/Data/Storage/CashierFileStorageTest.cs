// Filename: CashierFileStorageTest.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;

using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Infrastructure.Data.Storage
{
    public sealed class CashierFileStorageTest : UnitTest
    {


        [Fact]
        public void GetChallengePayouts_WithFiftySixRecords_ShouldHaveCountOfFiftySix()
        {
            // Arrange
            var storage = TestData.FileStorage;

            // Act
            var payoutStructures = storage.GetChallengePayouts();

            // Assert
            payoutStructures.SelectMany(payoutStructure => payoutStructure).Should().HaveCount(55);
        }

        public CashierFileStorageTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(testData, testMapper, testValidator)
        {
        }
    }
}
