// Filename: ValueObjectTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain.Aggregate;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Domain.Tests.Aggregate
{
    [TestClass]
    public sealed class ValueObjectTest
    {
        [TestMethod]
        public void OperatorEquals_DifferentProperty_ShouldBeFalse()
        {
            // Arrange
            var valueObject1 = new MockValueObjectWithProperty
            {
                Property = "Property1"
            };

            var valueObject2 = new MockValueObjectWithProperty
            {
                Property = "Property2"
            };

            // Act
            var condition = valueObject1 == valueObject2;

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void OperatorNotEquals_DifferentProperty_ShouldBeTrue()
        {
            // Arrange
            var valueObject1 = new MockValueObjectWithProperty
            {
                Property = "Property1"
            };

            var valueObject2 = new MockValueObjectWithProperty
            {
                Property = "Property2"
            };

            // Act
            var condition = valueObject1 != valueObject2;

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_SameProperty_ShouldBeTrue()
        {
            // Arrange
            var valueObject1 = new MockValueObjectWithProperty
            {
                Property = "Property"
            };

            var valueObject2 = new MockValueObjectWithProperty
            {
                Property = "Property"
            };

            // Act
            var condition = valueObject1.Equals(valueObject2);

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_DifferentProperty_ShouldBeFalse()
        {
            // Arrange
            var valueObject1 = new MockValueObjectWithProperty
            {
                Property = null
            };

            var valueObject2 = new MockValueObjectWithProperty
            {
                Property = "Property2"
            };

            // Act
            var condition = valueObject1.Equals(valueObject2);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_ReferenceEquals_ShouldBeTrue()
        {
            // Arrange
            var valueObject = new MockValueObjectWithProperty();

            // Act
            var condition = valueObject.Equals(valueObject);

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_DifferentType_ShouldBeFalse()
        {
            // Arrange
            var valueObject1 = new MockValueObjectWithProperty();

            var valueObject2 = new MockValueObjectWithProperties();

            // Act
            var condition = valueObject1.Equals(valueObject2);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void GetHashCode_SameProperty_ShouldBeEquals()
        {
            // Arrange
            var valueObject1 = new MockValueObjectWithProperty
            {
                Property = "Property"
            };

            var valueObject2 = new MockValueObjectWithProperty
            {
                Property = "Property"
            };

            // Act
            var hashCode1 = valueObject1.GetHashCode();
            var hashCode2 = valueObject2.GetHashCode();

            // Assert
            hashCode1.Should().Be(hashCode2);
        }

        [TestMethod]
        public void GetHashCode_DifferentProperty_ShouldNotBeEquals()
        {
            // Arrange
            var valueObject1 = new MockValueObjectWithProperty
            {
                Property = "Property1"
            };

            var valueObject2 = new MockValueObjectWithProperty
            {
                Property = "Property2"
            };

            // Act
            var hashCode1 = valueObject1.GetHashCode();
            var hashCode2 = valueObject2.GetHashCode();

            // Assert
            hashCode1.Should().NotBe(hashCode2);
        }

        private class MockValueObject : ValueObject
        {
            protected override IEnumerable<object> GetAtomicValues()
            {
                return Array.Empty<object>();
            }

            public override string ToString()
            {
                return string.Empty;
            }
        }

        private sealed class MockValueObjectWithProperty : MockValueObject
        {
            public string Property { get; set; }

            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return Property;
            }
        }

        private sealed class MockValueObjectWithProperties : MockValueObject
        {
            public string Property1 { get; set; }

            public string Property2 { get; set; }

            protected override IEnumerable<object> GetAtomicValues()
            {
                yield return Property1;
                yield return Property2;
            }
        }
    }
}