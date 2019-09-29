// Filename: ShippingFakerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.Providers.Stripe.Fakers;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Payment.UnitTests.Providers.Stripe.Fakers
{
    // TODO: These tests must be recast to be more explicit about the tested behavior. (Check if Bogus has a special librairy to test fakers.)
    public sealed class ShippingFakerTest : UnitTest
    {
        public ShippingFakerTest(TestDataFixture testData) : base(testData)
        {
        }

        [Fact]
        public void FakeShipping_ShouldNotBeNull()
        {
            // Arrange
            var shippingFaker = new ShippingFaker();

            // Act
            var shipping = shippingFaker.FakeShipping();

            // Assert
            shipping.Should().NotBeNull();
        }
    }
}
