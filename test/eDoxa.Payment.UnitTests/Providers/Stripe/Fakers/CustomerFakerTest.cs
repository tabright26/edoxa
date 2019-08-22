// Filename: CustomerFakerTest.cs
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
    public sealed class CustomerFakerTest
    {
        [TestMethod]
        public void FakeCustomer_ShouldNotBeNull()
        {
            // Arrange
            var customerFaker = new CustomerFaker();

            // Act
            var customer = customerFaker.FakeCustomer();

            // Assert
            customer.Should().NotBeNull();
        }
    }
}
