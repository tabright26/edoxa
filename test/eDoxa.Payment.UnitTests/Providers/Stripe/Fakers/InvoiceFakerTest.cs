// Filename: InvoiceFakerTest.cs
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
    public sealed class InvoiceFakerTest
    {
        [TestMethod]
        public void FakeInvoice_ShouldNotBeNull()
        {
            // Arrange
            var invoiceFaker = new InvoiceFaker();

            // Act
            var invoice = invoiceFaker.FakeInvoice();

            // Assert
            invoice.Should().NotBeNull();
        }
    }
}
