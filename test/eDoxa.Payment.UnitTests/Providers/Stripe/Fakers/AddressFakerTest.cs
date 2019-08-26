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
    // TODO: These tests must be recast to be more explicit about the tested behavior. (Check if Bogus has a special librairy to test fakers.)
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
