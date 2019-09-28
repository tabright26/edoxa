// Filename: InvoiceFakerTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.Providers.Stripe.Fakers;

using FluentAssertions;

using Xunit;

namespace eDoxa.Payment.UnitTests.Providers.Stripe.Fakers
{
    // TODO: These tests must be recast to be more explicit about the tested behavior. (Check if Bogus has a special librairy to test fakers.)
    public sealed class InvoiceFakerTest
    {
        [Fact]
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
