// Filename: PayoutFactoryTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Areas.Payouts.Factories;
using eDoxa.Cashier.Domain.Strategies;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Moq;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Areas.Challenges.Factories
{
    public sealed class PayoutFactoryTest : UnitTest
    {
        public PayoutFactoryTest(TestDataFixture testData, TestMapperFixture testMapper) : base(testData, testMapper)
        {
        }

        [Fact]
        public void CreateInstance_WithoutPayoutStrategy_ShouldNotBeNull()
        {
            // Arrange
            var payoutFactory = new PayoutFactory();

            // Act
            var payoutStrategy = payoutFactory.CreateInstance();

            // Assert
            payoutStrategy.Should().NotBeNull();
        }

        [Fact]
        public void CreateInstance_WithPayoutStrategy_ShouldNotBeNull()
        {
            // Arrange
            var mockPayoutStrategy = new Mock<IPayoutStrategy>();

            var payoutFactory = new PayoutFactory(mockPayoutStrategy.Object);

            // Act
            var payoutStrategy = payoutFactory.CreateInstance();

            // Assert
            payoutStrategy.Should().NotBeNull();
        }
    }
}
