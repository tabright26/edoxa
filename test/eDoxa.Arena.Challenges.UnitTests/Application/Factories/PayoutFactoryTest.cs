// Filename: PayoutFactoryTest.cs
// Date Created: 2019-06-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Arena.Challenges.Api.Application.Factories;
using eDoxa.Arena.Challenges.Domain.Strategies;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Arena.Challenges.UnitTests.Application.Factories
{
    // TODO: Should be a transfer to Cashier API.
    [TestClass]
    public sealed class PayoutFactoryTest
    {
        private Mock<IPayoutStrategy> _mockPayoutStrategy;

        [TestInitialize]
        public void TestInitialize()
        {
            _mockPayoutStrategy = new Mock<IPayoutStrategy>();
        }

        [TestMethod]
        public void CreateInstance_FromDependencyInjection_ShouldBePayoutStrategy()
        {
            // Arrange
            var payoutFactory = new PayoutFactory(_mockPayoutStrategy.Object);

            // Act
            var payoutStrategy = payoutFactory.CreateInstance();

            // Assert
            payoutStrategy.Should().Be(_mockPayoutStrategy.Object);
        }

        [TestMethod]
        public void CreateInstance_WithoutStrategy_ShouldNotBeNull()
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
