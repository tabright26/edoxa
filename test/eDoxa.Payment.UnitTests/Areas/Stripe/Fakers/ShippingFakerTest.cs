// Filename: ShippingFakerTest.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.Areas.Stripe.Fakers;
using eDoxa.Payment.TestHelpers;
using eDoxa.Payment.TestHelpers.Fixtures;

using FluentAssertions;

using Xunit;

namespace eDoxa.Payment.UnitTests.Areas.Stripe.Fakers
{
    // TODO: These tests must be recast to be more explicit about the tested behavior. (Check if Bogus has a special librairy to test fakers.)
    public sealed class ShippingFakerTest : UnitTest
    {
        public ShippingFakerTest(TestMapperFixture testMapper) : base(testMapper)
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
