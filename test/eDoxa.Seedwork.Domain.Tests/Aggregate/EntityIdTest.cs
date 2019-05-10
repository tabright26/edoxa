// Filename: EntityIdTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.ComponentModel;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Domain.Tests.Aggregate
{
    [TestClass]
    public sealed class EntityIdTest
    {
        [TestMethod]
        public void Value_NotEmptyGuid_ShouldBeValue()
        {
            // Arrange
            var value = Guid.NewGuid();

            // Act
            var entityId = MockEntityId.FromGuid(value);

            // Assert
            entityId.ToGuid().Should().Be(value);
        }

        [TestMethod]
        public void Value_EmptyGuid_ShouldThrowArgumentException()
        {
            // Arrange
            var value = Guid.Empty;

            // Act
            var action = new Action(() => MockEntityId.FromGuid(value));

            // Assert
            action.Should().Throw<ArgumentException>();
        }

        [TestMethod]
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

        [TestMethod]
        public void IsTransient_EmptyGuid_ShouldBeTrue()
        {
            // Arrange
            var entityId = new MockEntityId();

            // Act
            entityId.SetPrivateField("_value", Guid.Empty);

            // Assert
            entityId.IsTransient().Should().BeTrue();
        }

        [TestMethod]
        public void IsTransient_NotEmptyGuid_ShouldBeFalse()
        {
            // Act
            var entityId = new MockEntityId();

            // Assert
            entityId.IsTransient().Should().BeFalse();
        }

        [TestMethod]
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

        //[TestMethod]
        //public void ConvertFrom_InvalidString_ShouldBeNull()
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

        //    // Act
        //    var entityId = converter.ConvertFrom("This is an invalid string.");

        //    // Assert
        //    entityId.Should().BeNull();
        //}

        [TestMethod]
        public void ConvertFrom_Guid_ShouldBeValue()
        {
            // Arrange
            var value = Guid.NewGuid();
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var entityId = converter.ConvertFrom(value);

            // Assert
            entityId.As<MockEntityId>().ToGuid().Should().Be(value);
        }

        [DataRow(typeof(int))]
        [DataRow(typeof(long))]
        [DataTestMethod]
        public void ConvertFrom_InvalidType_ShouldThrowNotSupportedException(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var action = new Action(() => converter.ConvertFrom(type));

            // Assert
            action.Should().Throw<NotSupportedException>();
        }

        [DataRow(typeof(string))]
        [DataRow(typeof(Guid))]
        [DataTestMethod]
        public void CanConvertFrom_ValidType_ShouldBeTrue(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var isValid = converter.CanConvertFrom(type);

            // Assert
            isValid.Should().BeTrue();
        }

        [DataRow(typeof(int))]
        [DataRow(typeof(long))]
        [DataTestMethod]
        public void CanConvertFrom_ValidType_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var isValid = converter.CanConvertFrom(type);

            // Assert
            isValid.Should().BeFalse();
        }

        [DataRow(typeof(int))]
        [DataRow(typeof(long))]
        [DataTestMethod]
        public void CanConvertTo_ValidType_ShouldBeFalse(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var isValid = converter.CanConvertTo(type);

            // Assert
            isValid.Should().BeFalse();
        }

        [DataRow(typeof(string))]
        [DataRow(typeof(Guid))]
        [DataTestMethod]
        public void CanConvertTo_ValidType_ShouldBeTrue(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var isValid = converter.CanConvertTo(type);

            // Assert
            isValid.Should().BeTrue();
        }

        [TestMethod]
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

        [DataRow(typeof(string))]
        [DataRow(typeof(Guid))]
        [DataTestMethod]
        public void ConvertTo_ValidType_ShouldBeOfType(Type type)
        {
            // Arrange
            var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

            // Act
            var destination = converter.ConvertTo(new MockEntityId(), type);

            // Assert
            destination.Should().BeOfType(type);
        }

        //[TestMethod]
        //public void ConvertTo_InvalidValueType_ShouldBeNull()
        //{
        //    // Arrange
        //    var converter = TypeDescriptor.GetConverter(typeof(MockEntityId));

        //    // Act
        //    var destination = converter.ConvertTo(Guid.NewGuid(), typeof(Guid));

        //    // Assert
        //    destination.Should().BeNull();
        //}

        [DataRow(typeof(int))]
        [DataRow(typeof(long))]
        [DataTestMethod]
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
        }
    }
}