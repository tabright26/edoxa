// Filename: AddressFakerTest.cs
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
    public sealed class AddressFakerTest
    {
        [TestMethod]
        public void FakeAddress_ShouldNotBeNull()
        {
            // Arrange
            var addressFaker = new AddressFaker();

            // Act
            var address = addressFaker.FakeAddress();

            // Assert
            address.Should().NotBeNull();
        }
    }
}
