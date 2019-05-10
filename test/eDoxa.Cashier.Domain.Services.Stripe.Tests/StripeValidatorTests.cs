// Filename: StripeValidatorTests.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Cashier.Domain.Services.Stripe.Validators;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Domain.Services.Stripe.Tests
{
    [TestClass]
    public sealed class StripeValidatorTests
    {
        [DataRow("cus_23Eri2ee23")]
        [DataRow("cus_er34ri2ee23")]
        [DataTestMethod]
        public void Validate_InvalidFormat_ShouldNotThrowFormatException(string stripeId)
        {
            // Arrange
            var validator = new StripeIdValidator();

            // Act
            var action = new Action(() => validator.Validate(stripeId, "cus"));

            // Assert
            action.Should().NotThrow<FormatException>();
        }

        [DataRow("cus_23Eri2_ee23")]
        [DataRow("cus23Eri2ee23")]
        [DataTestMethod]
        public void Validate_InvalidFormat_ShouldThrowFormatException(string stripeId)
        {
            // Arrange
            var validator = new StripeIdValidator();

            // Act
             var action = new Action(() => validator.Validate(stripeId, "cus"));

            // Assert
            action.Should().Throw<FormatException>();
        }

        [DataRow("23Eri2_ee23")]
        [DataRow("test_23Eri2ee23")]
        [DataTestMethod]
        public void Validate_InvalidPrefix_ShouldThrowFormatException(string stripeId)
        {
            // Arrange
            var validator = new StripeIdValidator();

            // Act
            var action = new Action(() => validator.Validate(stripeId, "cus"));

            // Assert
            action.Should().Throw<FormatException>();
        }

        [DataRow("cus_we23we$%")]
        [DataRow("cus_@$Eri2ee23")]
        [DataRow("cus_trEr%2ee23")]
        [DataTestMethod]
        public void Validate_InvalidSuffix_ShouldThrowFormatException(string stripeId)
        {
            // Arrange
            var validator = new StripeIdValidator();

            // Act
            var action = new Action(() => validator.Validate(stripeId, "cus"));

            // Assert
            action.Should().Throw<FormatException>();
        }
    }
}