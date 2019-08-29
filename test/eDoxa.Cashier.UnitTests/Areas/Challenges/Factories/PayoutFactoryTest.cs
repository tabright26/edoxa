// Filename: PayoutFactoryTest.cs
// Date Created: 2019-08-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Areas.Challenges.Factories;
using eDoxa.Cashier.Domain.Strategies;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Cashier.UnitTests.Areas.Challenges.Factories
{
    [TestClass]
    public sealed class PayoutFactoryTest
    {
        [TestMethod]
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

        [TestMethod]
        public void CreateInstance_WithoutPayoutStrategy_ShouldNotBeNull()
        {
            // Arrange
            var payoutFactory = new PayoutFactory();

            // Act
            var payoutStrategy = payoutFactory.CreateInstance();

            // Assert
            payoutStrategy.Should().NotBeNull();
        }
    }
}
