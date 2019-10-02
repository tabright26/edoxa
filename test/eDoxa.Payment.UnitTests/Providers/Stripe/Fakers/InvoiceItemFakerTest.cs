﻿// Filename: InvoiceItemFakerTest.cs
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
    public sealed class InvoiceItemFakerTest : UnitTest
    {
        public InvoiceItemFakerTest(TestDataFixture testData) : base(testData)
        {
        }

        [Fact]
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
