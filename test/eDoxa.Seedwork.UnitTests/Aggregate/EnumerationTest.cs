// Filename: EnumerationTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.ComponentModel;

using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Xunit;

namespace eDoxa.Seedwork.UnitTests.Aggregate
{
    public sealed class EnumerationTest
    {
        [InlineData(1)]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(8)]
        [InlineData(16)]
        [InlineData("Enumeration1")]
        [InlineData("Enumeration2")]
        [InlineData("Enumeration4")]
        [InlineData("Enumeration8")]
        [InlineData("Enumeration16")]
        [Theory]
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

        [InlineData(0)]
        [InlineData(-1)]
        [InlineData("None")]
        [InlineData("All")]
        [Theory]
        public void ConvertFrom_AllOrNoneEnumeration_ShouldBeNone(object obj)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var enumeration = converter.ConvertFrom(obj);

            // Assert
            enumeration.As<MockEnumeration>().Should().Be(new MockEnumeration());
        }

        [InlineData(typeof(double))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(long))]
        [Theory]
        public void ConvertFrom_InvalidType_ShouldThrowNotSupportedException(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var action = new Action(() => converter.ConvertFrom(type));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }

        [InlineData(typeof(double))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(long))]
        [Theory]
        public void CanConvertFrom_ValidType_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var isValid = converter.CanConvertFrom(type);

            // Assert
            isValid.Should().BeFalse();
        }

        [InlineData(typeof(double))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(long))]
        [Theory]
        public void CanConvertTo_ValidType_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var isValid = converter.CanConvertTo(type);

            // Assert
            isValid.Should().BeFalse();
        }

        [InlineData(typeof(double))]
        [InlineData(typeof(decimal))]
        [InlineData(typeof(long))]
        [Theory]
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
            public static readonly MockEnumeration Enumeration1 = new MockEnumeration(1 << 0, nameof(Enumeration1));
            public static readonly MockEnumeration Enumeration2 = new MockEnumeration(1 << 1, nameof(Enumeration2));
            public static readonly MockEnumeration Enumeration4 = new MockEnumeration(1 << 2, nameof(Enumeration4));
            public static readonly MockEnumeration Enumeration8 = new MockEnumeration(1 << 3, nameof(Enumeration8));
            public static readonly MockEnumeration Enumeration16 = new MockEnumeration(1 << 4, nameof(Enumeration16));

            public MockEnumeration()
            {
            }

            private MockEnumeration(int value, string name) : base(value, name)
            {
            }
        }

        [Fact]
        public void CompareTo_Enumeration2WithEnumeration1_ShouldBeOne()
        {
            // Arrange
            var enumeration = MockEnumeration.Enumeration2;

            // Act
            var condition = enumeration.CompareTo(MockEnumeration.Enumeration1);

            // Assert
            condition.As<int>().Should().Be(1);
        }

        [Fact]
        public void ConvertFrom_InvalidEnumeration_ShouldBeValue()
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var enumeration = converter.ConvertFrom("InvalidEnumeration");

            // Assert
            enumeration.As<MockEnumeration>().Should().Be(new MockEnumeration());
        }

        [Fact]
        public void ConvertFrom_NullReference_ShouldBeNull()
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEnumeration));

            // Act
            var enumeration = converter.ConvertFrom(null);

            // Assert
            enumeration.As<MockEnumeration>().Should().BeNull();
        }

        [Fact]
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

        [Fact]
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

        [Fact]
        public void Enumeration_GetAll_ShouldHaveCountOfFive()
        {
            // Arrange
            const int expectedCount = 5;

            // Act
            var enumerations = Enumeration.GetEnumerations(typeof(MockEnumeration));

            // Assert
            enumerations.Should().HaveCount(expectedCount);
        }

        [Fact]
        public void Enumeration_GetTypes_ShouldContainAssembliesEnumerationTypes()
        {
            // Arrange
            var expectedEnumerationTypes = new[] {typeof(MockEnumeration)};

            // Act
            var enumerationTypes = Enumeration.GetTypes();

            // Assert
            enumerationTypes.Should().Contain(expectedEnumerationTypes);
        }

        [Fact]
        public void Equals_NullReference_ShouldBeFalse()
        {
            // Arrange
            var enumeration = new MockEnumeration();

            // Act
            var condition = enumeration.Equals(null);

            // Assert
            condition.Should().BeFalse();
        }

        [Fact]
        public void FromName_InvalidName_ShouldBeNone()
        {
            var expectedEnumeration = new MockEnumeration();

            var enumeration = MockEnumeration.FromName("None");

            enumeration.Should().Be(expectedEnumeration);
        }

        [Fact]
        public void FromName_ValidName_ShouldBeNone()
        {
            var expectedEnumeration = MockEnumeration.Enumeration1;

            var enumeration = MockEnumeration.FromName(expectedEnumeration.Name);

            enumeration.Should().Be(expectedEnumeration);
        }

        [Fact]
        public void FromValue_InvalidValue_ShouldBeNone()
        {
            var expectedEnumeration = new MockEnumeration();

            var enumeration = MockEnumeration.FromValue(default);

            enumeration.Should().Be(expectedEnumeration);
        }

        [Fact]
        public void FromValue_ValidValue_ShouldBeNone()
        {
            var expectedEnumeration = MockEnumeration.Enumeration1;

            var enumeration = MockEnumeration.FromValue(expectedEnumeration.Value);

            enumeration.Should().Be(expectedEnumeration);
        }

        [Fact]
        public void HasEnumeration_AllEnumeration_ShouldBeFalse()
        {
            var expectedEnumeration = MockEnumeration.All;

            var condition = MockEnumeration.HasEnumeration(expectedEnumeration);

            condition.Should().BeFalse();
        }

        [Fact]
        public void HasEnumeration_NoneEnumeration_ShouldBeFalse()
        {
            var expectedEnumeration = new MockEnumeration();

            var condition = MockEnumeration.HasEnumeration(expectedEnumeration);

            condition.Should().BeFalse();
        }

        [Fact]
        public void HasEnumeration_ValidEnumeration_ShouldBeTrue()
        {
            var expectedEnumeration = MockEnumeration.Enumeration1;

            var condition = MockEnumeration.HasEnumeration(expectedEnumeration);

            condition.Should().BeTrue();
        }

        [Fact]
        public void HasFilter_AllEnumeration_ShouldBeTrue()
        {
            var enumeration = MockEnumeration.Enumeration1;

            var condition = enumeration.HasFilter(MockEnumeration.All);

            condition.Should().BeTrue();
        }

        [Fact]
        public void HasFilter_InvalidEnumeration_ShouldBeFalse()
        {
            var enumeration = MockEnumeration.Enumeration1;

            var condition = enumeration.HasFilter(MockEnumeration.Enumeration2);

            condition.Should().BeFalse();
        }

        [Fact]
        public void HasFilter_NoneEnumeration_ShouldBeFalse()
        {
            var enumeration = MockEnumeration.Enumeration1;

            var condition = enumeration.HasFilter(new MockEnumeration());

            condition.Should().BeFalse();
        }

        [Fact]
        public void HasFilter_ValidEnumeration_ShouldBeTrue()
        {
            var expectedEnumeration = MockEnumeration.Enumeration1;

            var condition = expectedEnumeration.HasFilter(expectedEnumeration);

            condition.Should().BeTrue();
        }

        [Fact]
        public void Public_Constructor_ShouldBeNone()
        {
            // Arrange
            var enumeration = new MockEnumeration();

            // Assert
            enumeration.Value.Should().Be(default);
            enumeration.Name.Should().Be("None");
        }
    }
}
