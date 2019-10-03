// Filename: TransferFakerTest.cs
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
    public sealed class TransferFakerTest : UnitTest
    {
        public TransferFakerTest(TestDataFixture testData) : base(testData)
        {
        }

        [Fact]
        public void FakeTransfer_ShouldNotBeNull()
        {
            // Arrange
            var transferFaker = new TransferFaker();

            // Act
            var transfer = transferFaker.FakeTransfer();

            // Assert
            transfer.Should().NotBeNull();
        }
    }
}
