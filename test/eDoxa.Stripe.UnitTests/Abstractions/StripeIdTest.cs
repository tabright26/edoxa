// Filename: StripeIdTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel;

using eDoxa.Stripe.Exceptions;
using eDoxa.Stripe.Models;
using eDoxa.Stripe.UnitTests.Utilities;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Stripe.UnitTests.Abstractions
{
    [TestClass]
    public sealed class StripeIdTest
    {
        private static readonly StripeBuilder StripeBuilder = StripeBuilder.Instance;

        [DataRow(typeof(StripeConnectAccountId), "acct_qwe23okqwe123")]
        [DataRow(typeof(StripeBankAccountId), "ba_qwe23okqwe123")]
        [DataRow(typeof(StripeCardId), "card_qwe23okqwe123")]
        [DataRow(typeof(StripeCustomerId), "cus_qwe23okqwe123")]
        [DataTestMethod]
        public void ConvertFrom_ShouldBeOfType(Type type, string source)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(type);

            // Act
            var destination = converter.ConvertFrom(source);

            // Assert
            destination.Should().BeOfType(type);
        }

        [DataRow(typeof(StripeConnectAccountId), "ba_qwe23okqwe123")]
        [DataRow(typeof(StripeBankAccountId), "acct_qwe23okqwe123")]
        [DataRow(typeof(StripeCardId), "cardqwe23okqwe123")]
        [DataRow(typeof(StripeCustomerId), "cus_qwe23okq_we123")]
        [DataTestMethod]
        public void ConvertFrom_ShouldThrowStripeIdException(Type type, string source)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(type);

            // Act
            var action = new Action(() => converter.ConvertFrom(source));

            // Assert
            action.Should().Throw<StripeIdException>();
        }

        [DataRow(typeof(StripeConnectAccountId), 1000)]
        [DataRow(typeof(StripeBankAccountId), 112323L)]
        [DataRow(typeof(StripeCardId), 12312312.123D)]
        [DataRow(typeof(StripeCustomerId), 12421412.123F)]
        [DataTestMethod]
        public void ConvertFrom_ShouldThrowNotSupportedException(Type type, object source)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(type);

            // Act
            var action = new Action(() => converter.ConvertFrom(source));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }

        [DataRow(typeof(string))]
        [DataTestMethod]
        public void CanConvertFrom_ShouldBeTrue(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(StripeConnectAccountId));

            // Act
            var condition = converter.CanConvertFrom(type);

            // Assert
            condition.Should().BeTrue();
        }

        [DataRow(typeof(int))]
        [DataRow(typeof(long))]
        [DataRow(typeof(Guid))]
        [DataTestMethod]
        public void CanConvertFrom_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(StripeConnectAccountId));

            // Act
            var condition = converter.CanConvertFrom(type);

            // Assert
            condition.Should().BeFalse();
        }

        [DataRow(typeof(string))]
        [DataTestMethod]
        public void CanConvertTo_ShouldBeTrue(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(StripeConnectAccountId));

            // Act
            var condition = converter.CanConvertTo(type);

            // Assert
            condition.Should().BeTrue();
        }

        [DataRow(typeof(int))]
        [DataRow(typeof(long))]
        [DataRow(typeof(Guid))]
        [DataTestMethod]
        public void CanConvertTo_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(StripeConnectAccountId));

            // Act
            var condition = converter.CanConvertTo(type);

            // Assert
            condition.Should().BeFalse();
        }

        [DataRow(typeof(string))]
        [DataTestMethod]
        public void ConvertTo_ShouldBeOfType(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(StripeConnectAccountId));

            // Act
            var destination = converter.ConvertTo(StripeBuilder.CreateAccountId(), type);

            // Assert
            destination.Should().BeOfType(type);
        }

        [DataRow(typeof(int))]
        [DataRow(typeof(long))]
        [DataRow(typeof(Guid))]
        [DataTestMethod]
        public void ConvertTo_ShouldThrowNotSupportedException(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(StripeConnectAccountId));

            // Act
            var action = new Action(() => converter.ConvertTo(StripeBuilder.CreateAccountId(), type));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }
    }
}
