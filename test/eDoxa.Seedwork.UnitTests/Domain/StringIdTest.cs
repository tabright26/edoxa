// Filename: StringIdTest.cs
// Date Created: 2019-10-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.ComponentModel;

using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Xunit;

namespace eDoxa.Seedwork.UnitTests.Domain
{
    public sealed class StringIdTest
    {
        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        [Theory]
        public void ConvertFrom_InvalidType_ShouldThrowNotSupportedException(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockStringId));

            // Act
            var action = new Action(() => converter.ConvertFrom(type));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }

        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        [Theory]
        public void CanConvertFrom_ValidType_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockStringId));

            // Act
            var isValid = converter.CanConvertFrom(type);

            // Assert
            isValid.Should().BeFalse();
        }

        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        [Theory]
        public void CanConvertTo_ValidType_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockStringId));

            // Act
            var isValid = converter.CanConvertTo(type);

            // Assert
            isValid.Should().BeFalse();
        }

        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        [Theory]
        public void ConvertTo_InvalidType_ShouldThrowNotSupportedException(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockStringId));

            // Act
            var action = new Action(() => converter.ConvertTo(new MockStringId(), type));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }

        [TypeConverter(typeof(StringIdTypeConverter))]
        private sealed class MockStringId : StringId<MockStringId>
        {
            public MockStringId(string stringId) : this()
            {
                Value = stringId;
            }

            public MockStringId()
            {
            }
        }

        [Fact]
        public void ConvertFrom_NullReference_ShouldBeValue()
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockStringId));

            // Act
            var stringId = converter.ConvertFrom(null);

            // Assert
            stringId.As<MockStringId>().IsTransient().Should().BeTrue();
        }

        [Fact]
        public void ConvertFrom_ValidString_ShouldBeValue()
        {
            // Arrange
            var value = Guid.NewGuid().ToString();
            var converter = TypeDescriptor.GetConverter(typeof(MockStringId));

            // Act
            var stringId = converter.ConvertFrom(value);

            // Assert
            stringId.As<MockStringId>().ToString().Should().Be(value);
        }

        [Fact]
        public void ConvertTo_String_ShouldBeValue()
        {
            // Arrange
            var value = Guid.NewGuid().ToString();
            var converter = TypeDescriptor.GetConverter(typeof(MockStringId));

            // Act
            var stringId = converter.ConvertFrom(value);

            // Assert
            stringId.As<MockStringId>().ToString().Should().Be(value);
        }

        [Fact]
        public void Equals_SameType_ShouldBeFalse()
        {
            // Arrange
            var stringId1 = new MockStringId();
            var stringId2 = new MockStringId();

            // Act
            var condition = stringId1.Equals(stringId2);

            // Assert
            condition.Should().BeFalse();
        }

        [Fact]
        public void IsTransient_NotEmptyGuid_ShouldBeFalse()
        {
            // Act
            var stringId = new MockStringId();

            // Assert
            stringId.IsTransient().Should().BeFalse();
        }

        [Fact]
        public void Value_InvalidGuid_ShouldBeValue()
        {
            // Arrange
            var guid = Guid.NewGuid().ToString();
            var stringId1 = new MockStringId(guid);
            var stringId2 = new MockStringId(guid);

            // Act
            var condition = stringId1.Equals(stringId2);

            // Assert
            condition.Should().BeTrue();
        }

        [Fact]
        public void Value_NotEmptyGuid_ShouldBeValue()
        {
            // Arrange
            var value = Guid.NewGuid().ToString();

            // Act
            var stringId = MockStringId.Parse(value);

            // Assert
            stringId.ToString().Should().Be(value);
        }
    }
}
