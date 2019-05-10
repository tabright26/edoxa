// Filename: StripeIdTest.cs
// Date Created: 2019-04-09
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.ComponentModel;

using eDoxa.Cashier.Domain.Services.Stripe.Abstractions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Cashier.Domain.Services.Stripe.Tests
{
    [TestClass]
    public sealed class StripeIdTest
    {
        //[DataRow("mock_qwe23Ert2e23")]
        //[DataTestMethod]
        //public void Parse_ValidInput_ShouldNotThrowFormatException(string input)
        //{
        //    // Act
        //    var action = new Action(() => MockStripeId.Parse(input));

        //    // Assert
        //    action.Should().NotThrow<FormatException>();
        //}

        //[DataRow("qwe23Ert2e23")]
        //[DataRow("qwe_23Ert_2e23")]
        //[DataRow("mock_qwe2rt%^2e23")]
        //[DataTestMethod]
        //public void Parse_InvalidInput_ShouldThrowFormatException(string input)
        //{
        //    // Act
        //    var action = new Action(() => MockStripeId.Parse(input));

        //    // Assert
        //    action.Should().Throw<FormatException>();
        //}

        //[TestMethod]
        //public void ConvertFrom_ValidString_ShouldBeValue()
        //{
        //    // Arrange
        //    var value = CreateStripeId().ToString();
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var entityId = converter.ConvertFrom(value);

        //    // Assert
        //    entityId.As<MockStripeId>().ToString().Should().Be(value);
        //}

        //[DataRow(typeof(int))]
        //[DataRow(typeof(long))]
        //[DataTestMethod]
        //public void ConvertFrom_InvalidType_ShouldThrowNotSupportedException(Type type)
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var action = new Action(() => converter.ConvertFrom(type));

        //    // Assert
        //    action.Should().Throw<NotSupportedException>();
        //}

        //[DataRow(typeof(string))]
        //[DataTestMethod]
        //public void CanConvertFrom_ValidType_ShouldBeTrue(Type type)
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var isValid = converter.CanConvertFrom(type);

        //    // Assert
        //    isValid.Should().BeTrue();
        //}

        //[DataRow(typeof(int))]
        //[DataRow(typeof(long))]
        //[DataTestMethod]
        //public void CanConvertFrom_ValidType_ShouldBeFalse(Type type)
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var isValid = converter.CanConvertFrom(type);

        //    // Assert
        //    isValid.Should().BeFalse();
        //}

        //[DataRow(typeof(int))]
        //[DataRow(typeof(long))]
        //[DataTestMethod]
        //public void CanConvertTo_ValidType_ShouldBeFalse(Type type)
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var isValid = converter.CanConvertTo(type);

        //    // Assert
        //    isValid.Should().BeFalse();
        //}

        //[DataRow(typeof(string))]
        //[DataTestMethod]
        //public void CanConvertTo_ValidType_ShouldBeTrue(Type type)
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var isValid = converter.CanConvertTo(type);

        //    // Assert
        //    isValid.Should().BeTrue();
        //}

        //[TestMethod]
        //public void ConvertTo_String_ShouldBeValue()
        //{
        //    // Arrange
        //    var value = CreateStripeId().ToString();
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var entityId = converter.ConvertFrom(value);

        //    // Assert
        //    entityId.As<MockStripeId>().ToString().Should().Be(value);
        //}

        //[DataRow(typeof(string))]
        //[DataTestMethod]
        //public void ConvertTo_ValidType_ShouldBeOfType(Type type)
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var destination = converter.ConvertTo(CreateStripeId(), type);

        //    // Assert
        //    destination.Should().BeOfType(type);
        //}

        //[TestMethod]
        //public void ConvertTo_InvalidValueType_ShouldBeNull()
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var destination = converter.ConvertTo(Guid.NewGuid(), typeof(Guid));

        //    // Assert
        //    destination.Should().BeNull();
        //}

        //[DataRow(typeof(int))]
        //[DataRow(typeof(long))]
        //[DataTestMethod]
        //public void ConvertTo_InvalidType_ShouldThrowNotSupportedException(Type type)
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockStripeId));

        //    // Act
        //    var action = new Action(() => converter.ConvertTo(CreateStripeId(), type));

        //    // Assert
        //    action.Should().Throw<NotSupportedException>();
        //}

        private static MockStripeId CreateStripeId()
        {

            return MockStripeId.Parse("mock_er23RER123w");
        }

        [TypeConverter(typeof(StripeIdTypeConverter))]
        private sealed class MockStripeId : StripeId<MockStripeId>
        {
            private const string Prefix = "mock";

            public MockStripeId() : base(Prefix)
            {
            }
        }
    }
}