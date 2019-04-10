﻿// Filename: DomainSignatureTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Seedwork.Domain.Reflexion;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Domain.Tests.Reflection
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
        public void Type_NullType_ShouldThrowArgumentNullException()
        {
            // Act
            var action = new Action(() => new DomainSignature(null, Array.Empty<PropertyInfo>()));

            // Assert
            action.Should().Throw<ArgumentNullException>();
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
        public void Properties_NullReference_ShouldBeEmpty()
        {
            // Act
            var signature = new DomainSignature(typeof(MockBaseObject1), null);

            // Assert
            signature.Properties.Should().BeEmpty();
        }

        [TestMethod]
        public void Equals_NullReference_ShouldBeFalse()
        {
            // Arrange
            var signature = new DomainSignature(typeof(MockBaseObject1), Array.Empty<PropertyInfo>());

            // Act
            var condition = signature.Equals(null);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_ReferenceEquals_ShouldBeTrue()
        {
            // Arrange
            var signature = new DomainSignature(typeof(MockBaseObject1), Array.Empty<PropertyInfo>());

            // Act
            var condition = signature.Equals(signature);

            // Assert
            condition.Should().BeTrue();
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