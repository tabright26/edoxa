// Filename: ChallengePayoutFactoryTest.cs
// Date Created: 2019-12-26
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Cashier.Api.Application.Factories;
using eDoxa.Cashier.TestHelper;
using eDoxa.Cashier.TestHelper.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Cashier.UnitTests.Application.Factories
{
    public sealed class ChallengePayoutFactoryTest : UnitTest
    {
        public ChallengePayoutFactoryTest(TestDataFixture testData, TestMapperFixture testMapper, TestValidator testValidator) : base(
            testData,
            testMapper,
            testValidator)
        {
        }

        [Fact]
        public void CreateInstance_WithoutPayoutStrategy_ShouldNotBeNull()
        {
            // Arrange
            var payoutFactory = new ChallengePayoutFactory();

            // Act
            var payoutStrategy = payoutFactory.CreateInstance();

            // Assert
            payoutStrategy.Should().NotBeNull();
        }

        [Fact]
        public void CreateInstance_WithPayoutStrategy_ShouldNotBeNull()
        {
            // Arrange
            var payoutFactory = new ChallengePayoutFactory(TestMock.ChallengePayoutStrategy.Object);

            // Act
            var payoutStrategy = payoutFactory.CreateInstance();

            // Assert
            payoutStrategy.Should().NotBeNull();
        }
    }
}
