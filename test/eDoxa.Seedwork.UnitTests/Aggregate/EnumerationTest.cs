// Filename: EnumerationTest.cs
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

using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Attributes;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.Aggregate
{
    [TestClass]
    public sealed class EnumerationTest
    {
        [TestMethod]
        public void Enumeration_GetTypes_ShouldContainAssembliesEnumerationTypes()
        {
            // Arrange
            var expectedEnumerationTypes = new[] {typeof(CurrencyType), typeof(Game), typeof(MockEnumeration)};

            // Act
            var enumerationTypes = Enumeration.GetTypes();

            // Assert
            enumerationTypes.Should().Contain(expectedEnumerationTypes);
        }

        [TestMethod]
        public void Enumeration_GetAll_ShouldHaveCountOfFive()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var enumerations = Enumeration.GetAll(typeof(MockEnumeration));

            // Assert
            enumerations.Should().HaveCount(expectedCount);
        }

        [TestMethod]
        public void Public_Constructor_ShouldBeNone()
        {
            // Arrange
            var enumeration = new MockEnumeration();

            // Assert
            enumeration.Value.Should().Be(default);
            enumeration.Name.Should().Be("None");
        }

        [TestMethod]
        public void CompareTo_Enumeration2WithEnumeration1_ShouldBeOne()
        {
            // Arrange
            var enumeration = MockEnumeration.Enumeration2;

            // Act
            var condition = enumeration.CompareTo(MockEnumeration.Enumeration1);

            // Assert
            condition.As<int>().Should().Be(1);
        }

        [TestMethod]
        public void Equals_NullReference_ShouldBeFalse()
        {
            // Arrange
            var enumeration = new MockEnumeration();

            // Act
            var condition = enumeration.Equals(null);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void FromValue_ValidValue_ShouldBeNone()
        {
            var expectedEnumeration = MockEnumeration.Enumeration1;

            var enumeration = MockEnumeration.FromValue(expectedEnumeration.Value);

            enumeration.Should().Be(expectedEnumeration);
        }

        [TestMethod]
        public void FromValue_InvalidValue_ShouldBeNone()
        {
            var expectedEnumeration = new MockEnumeration();

            var enumeration = MockEnumeration.FromValue(default);

            enumeration.Should().Be(expectedEnumeration);
        }

        [TestMethod]
        public void FromName_ValidName_ShouldBeNone()
        {
            var expectedEnumeration = MockEnumeration.Enumeration1;

            var enumeration = MockEnumeration.FromName(expectedEnumeration.Name);

            enumeration.Should().Be(expectedEnumeration);
        }

        [TestMethod]
        public void FromName_InvalidName_ShouldBeNone()
        {
            var expectedEnumeration = new MockEnumeration();

            var enumeration = MockEnumeration.FromName("None");

            enumeration.Should().Be(expectedEnumeration);
        }

        [TestMethod]
        public void HasEnumeration_ValidEnumeration_ShouldBeTrue()
        {
            var expectedEnumeration = MockEnumeration.Enumeration1;

            var condition = MockEnumeration.HasEnumeration(expectedEnumeration);

            condition.Should().BeTrue();
        }

        [TestMethod]
        public void HasEnumeration_NoneEnumeration_ShouldBeFalse()
        {
            var expectedEnumeration = new MockEnumeration();

            var condition = MockEnumeration.HasEnumeration(expectedEnumeration);

            condition.Should().BeFalse();
        }

        [TestMethod]
        public void HasEnumeration_AllEnumeration_ShouldBeFalse()
        {
            var expectedEnumeration = MockEnumeration.All;

            var condition = MockEnumeration.HasEnumeration(expectedEnumeration);

            condition.Should().BeFalse();
        }

        [TestMethod]
        public void HasFilter_ValidEnumeration_ShouldBeTrue()
        {
            var expectedEnumeration = MockEnumeration.Enumeration1;

            var condition = expectedEnumeration.HasFilter(expectedEnumeration);

            condition.Should().BeTrue();
        }

        [TestMethod]
        public void HasFilter_InvalidEnumeration_ShouldBeFalse()
        {
            var enumeration = MockEnumeration.Enumeration1;

            var condition = enumeration.HasFilter(MockEnumeration.Enumeration2);

            condition.Should().BeFalse();
        }

        [TestMethod]
        public void HasFilter_AllEnumeration_ShouldBeTrue()
        {
            var enumeration = MockEnumeration.Enumeration1;

            var condition = enumeration.HasFilter(MockEnumeration.All);

            condition.Should().BeTrue();
        }

        [TestMethod]
        public void HasFilter_NoneEnumeration_ShouldBeFalse()
        {
            var enumeration = MockEnumeration.Enumeration1;

            var condition = enumeration.HasFilter(new MockEnumeration());

            condition.Should().BeFalse();
        }

        [DataRow(1)]
        [DataRow(2)]
        [DataRow(4)]
        [DataRow(8)]
        [DataRow(16)]
        [DataRow("Enumeration1")]
        [DataRow("Enumeration2")]
        [DataRow("Enumeration4")]
        [DataRow("Enumeration8")]
        [DataRow("Enumeration16")]
        [DataTestMethod]
        public void ConvertFrom_ValidEnumeration_ShouldBeEnumeration(object obj)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var enumeration = converter.ConvertFrom(obj);

            // Assert
            enumeration.As<MockEnumeration>().Should().NotBe(new MockEnumeration());
            enumeration.As<MockEnumeration>().Should().NotBe(MockEnumeration.All);
        }

        [DataRow(0)]
        [DataRow(-1)]
        [DataRow("None")]
        [DataRow("All")]
        [DataTestMethod]
        public void ConvertFrom_AllOrNoneEnumeration_ShouldBeNone(object obj)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var enumeration = converter.ConvertFrom(obj);

            // Assert
            enumeration.As<MockEnumeration>().Should().Be(new MockEnumeration());
        }

        [TestMethod]
        public void ConvertFrom_InvalidEnumeration_ShouldBeValue()
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var enumeration = converter.ConvertFrom("InvalidEnumeration");

            // Assert
            enumeration.As<MockEnumeration>().Should().Be(new MockEnumeration());
        }

        [TestMethod]
        public void ConvertFrom_NullReference_ShouldBeNull()
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var enumeration = converter.ConvertFrom(null);

            // Assert
            enumeration.As<MockEnumeration>().Should().BeNull();
        }

        [DataRow(typeof(double))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(long))]
        [DataTestMethod]
        public void ConvertFrom_InvalidType_ShouldThrowNotSupportedException(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var action = new Action(() => converter.ConvertFrom(type));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }

        [DataRow(typeof(double))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(long))]
        [DataTestMethod]
        public void CanConvertFrom_ValidType_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var isValid = converter.CanConvertFrom(type);

            // Assert
            isValid.Should().BeFalse();
        }

        [DataRow(typeof(double))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(long))]
        [DataTestMethod]
        public void CanConvertTo_ValidType_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var isValid = converter.CanConvertTo(type);

            // Assert
            isValid.Should().BeFalse();
        }

        [TestMethod]
        public void ConvertTo_EnumerationValue_ShouldBeEnumeration()
        {
            // Arrange
            var mockEnumeration = MockEnumeration.Enumeration1;

            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var enumeration = converter.ConvertTo(mockEnumeration, typeof(int));

            // Assert
            enumeration.As<int>().Should().Be(mockEnumeration.Value);
        }

        [TestMethod]
        public void ConvertTo_EnumerationName_ShouldBeEnumeration()
        {
            // Arrange
            var mockEnumeration = MockEnumeration.Enumeration1;

            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var enumeration = converter.ConvertTo(mockEnumeration, typeof(string));

            // Assert
            enumeration.As<string>().Should().Be(mockEnumeration.Name);
        }

        [DataRow(typeof(double))]
        [DataRow(typeof(decimal))]
        [DataRow(typeof(long))]
        [DataTestMethod]
        public void ConvertTo_InvalidType_ShouldThrowNotSupportedException(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var action = new Action(() => converter.ConvertTo(new MockEnumeration(), type));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }

        [TypeConverter(typeof(EnumerationTypeConverter))]
        private sealed class MockEnumeration : Enumeration<MockEnumeration>
        {
            [AllowValue(true)] public static readonly MockEnumeration Enumeration1 = new MockEnumeration(1 << 0, nameof(Enumeration1));
            [AllowValue(true)] public static readonly MockEnumeration Enumeration2 = new MockEnumeration(1 << 1, nameof(Enumeration2));
            [AllowValue(true)] public static readonly MockEnumeration Enumeration4 = new MockEnumeration(1 << 2, nameof(Enumeration4));
            [AllowValue(true)] public static readonly MockEnumeration Enumeration8 = new MockEnumeration(1 << 3, nameof(Enumeration8));
            [AllowValue(false)] public static readonly MockEnumeration Enumeration16 = new MockEnumeration(1 << 4, nameof(Enumeration16));

            public MockEnumeration()
            {
            }

            private MockEnumeration(int value, string name) : base(value, name)
            {
            }
        }
    }
}
