// Filename: DomainSignatureTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Reflection;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.Reflection
{
    [TestClass]
    public sealed class DomainSignatureTest
    {
        [TestMethod]
        public void Type_NotNullType_ShouldBeType()
        {
            // Arrange
            var type = typeof(MockBaseObject1);

            // Act
            var signature = new DomainSignature(type, Array.Empty<PropertyInfo>());

            // Assert
            signature.Type.Should().Be(type);
        }

        [TestMethod]
        public void Properties_EmptyArray_ShouldBeEmpty()
        {
            // Arrange
            var properties = Array.Empty<PropertyInfo>();

            // Act
            var signature = new DomainSignature(typeof(MockBaseObject1), properties);

            // Assert
            signature.Properties.Should().BeEmpty();
        }

        [TestMethod]
        public void Properties_NotEmptyArray_ShouldNotBeEmpty()
        {
            // Arrange
            var properties = new PropertyInfo[5];

            // Act
            var signature = new DomainSignature(typeof(MockBaseObject1), properties);

            // Assert
            signature.Properties.Should().NotBeEmpty();
        }

        [TestMethod]
        public void Equals_SameType_ShouldBeTrue()
        {
            // Arrange
            var signature1 = new DomainSignature(typeof(MockBaseObject1), Array.Empty<PropertyInfo>());
            var signature2 = new DomainSignature(typeof(MockBaseObject1), Array.Empty<PropertyInfo>());

            // Act
            var condition = signature1.Equals(signature2);

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_DifferentType_ShouldBeFalse()
        {
            // Arrange
            var signature1 = new DomainSignature(typeof(MockBaseObject1), Array.Empty<PropertyInfo>());
            var signature2 = new DomainSignature(typeof(MockBaseObject2), Array.Empty<PropertyInfo>());

            // Act
            var condition = signature1.Equals(signature2);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void GetHashCode_NotNullType_ShouldBeTypeHashCode()
        {
            // Arrange
            var type = typeof(MockBaseObject1);
            var signature = new DomainSignature(type, Array.Empty<PropertyInfo>());

            // Act
            var hashCode = signature.GetHashCode();

            // Assert
            hashCode.Should().Be(type.GetHashCode());
        }

        private sealed class MockBaseObject1 : BaseObject
        {
            protected override PropertyInfo[] TypeSignatureProperties()
            {
                return Array.Empty<PropertyInfo>();
            }
        }

        private sealed class MockBaseObject2 : BaseObject
        {
            protected override PropertyInfo[] TypeSignatureProperties()
            {
                return Array.Empty<PropertyInfo>();
            }
        }
    }
}
