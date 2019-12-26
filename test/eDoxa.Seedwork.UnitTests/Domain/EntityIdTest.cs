// Filename: EntityIdTest.cs
// Date Created: 2019-09-16
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
    public sealed class EntityIdTest
    {
        [InlineData(typeof(int))]
        [InlineData(typeof(long))]
        [Theory]
        public void ConvertFrom_InvalidType_ShouldThrowNotSupportedException(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

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
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

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
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

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
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var action = new Action(() => converter.ConvertTo(new MockEntityId(), type));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }

        [TypeConverter(typeof(EntityIdTypeConverter))]
        private sealed class MockEntityId : EntityId<MockEntityId>
        {
            public MockEntityId(Guid entityId) : this()
            {
                Value = entityId;
            }

            public MockEntityId()
            {
            }
        }

        [Fact]
        public void ConvertFrom_InvalidString_ShouldBeValue()
        {
            // Arrange
            const string value = "qeqw12312";
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var entityId = converter.ConvertFrom(value);

            // Assert
            entityId.As<MockEntityId>().IsTransient().Should().BeTrue();
        }

        [Fact]
        public void ConvertFrom_NullReference_ShouldBeValue()
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var entityId = converter.ConvertFrom(null);

            // Assert
            entityId.As<MockEntityId>().IsTransient().Should().BeTrue();
        }

        [Fact]
        public void ConvertFrom_ValidString_ShouldBeValue()
        {
            // Arrange
            var value = Guid.NewGuid().ToString();
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var entityId = converter.ConvertFrom(value);

            // Assert
            entityId.As<MockEntityId>().ToString().Should().Be(value);
        }

        [Fact]
        public void ConvertTo_String_ShouldBeValue()
        {
            // Arrange
            var value = Guid.NewGuid().ToString();
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var entityId = converter.ConvertFrom(value);

            // Assert
            entityId.As<MockEntityId>().ToString().Should().Be(value);
        }

        [Fact]
        public void Equals_SameType_ShouldBeFalse()
        {
            // Arrange
            var entityId1 = new MockEntityId();
            var entityId2 = new MockEntityId();

            // Act
            var condition = entityId1.Equals(entityId2);

            // Assert
            condition.Should().BeFalse();
        }

        [Fact]
        public void IsTransient_NotEmptyGuid_ShouldBeFalse()
        {
            // Act
            var entityId = new MockEntityId();

            // Assert
            entityId.IsTransient().Should().BeFalse();
        }

        [Fact]
        public void Value_InvalidGuid_ShouldBeValue()
        {
            // Arrange
            var guid = Guid.NewGuid();
            var entityId1 = new MockEntityId(guid);
            var entityId2 = new MockEntityId(guid);

            // Act
            var condition = entityId1.Equals(entityId2);

            // Assert
            condition.Should().BeTrue();
        }

        [Fact]
        public void Value_NotEmptyGuid_ShouldBeValue()
        {
            // Arrange
            var value = Guid.NewGuid();

            // Act
            var entityId = MockEntityId.FromGuid(value);

            // Assert
            entityId.ToGuid().Should().Be(value);
        }
    }
}
