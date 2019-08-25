// Filename: TransferFakerTest.cs
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
    public sealed class TransferFakerTest
    {
        [TestMethod]
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
