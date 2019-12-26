// Filename: ValueObjectTest.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Domain;

using FluentAssertions;

using Xunit;

namespace eDoxa.Seedwork.UnitTests.Domain
{
    public sealed class ValueObjectTest
    {
        private class MockValueObject : ValueObject
        {
            protected override IEnumerable<object> GetAtomicValues()
            {
                return Array.Empty<object>();
            }

            public override string ToString()
            {
                return typeof(MockValueObject).ToString();
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

        [Fact]
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

        [Fact]
        public void Equals_DifferentType_ShouldBeFalse()
        {
            // Arrange
            var valueObject1 = new MockValueObjectWithProperty();

            var valueObject2 = new MockValueObjectWithProperties();

            // Act
            var condition = valueObject1 == valueObject2;

            // Assert
            condition.Should().BeFalse();
        }

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
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

        [Fact]
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
    }
}
