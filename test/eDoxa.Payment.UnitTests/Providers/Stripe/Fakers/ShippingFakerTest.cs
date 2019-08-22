// Filename: ShippingFakerTest.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.Providers.Stripe.Fakers;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Payment.UnitTests.Providers.Stripe.Fakers
{
    [TestClass]
    public sealed class ShippingFakerTest
    {
        [TestMethod]
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
