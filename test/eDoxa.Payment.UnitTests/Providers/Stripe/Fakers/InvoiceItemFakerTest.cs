// Filename: InvoiceItemFakerTest.cs
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
    public sealed class InvoiceItemFakerTest
    {
        [TestMethod]
        public void FakeInvoiceItem_ShouldNotBeNull()
        {
            // Arrange
            var invoiceItemFaker = new InvoiceItemFaker();

            // Act
            var invoiceItem = invoiceItemFaker.FakeInvoiceItem();

            // Assert
            invoiceItem.Should().NotBeNull();
        }
    }
}
