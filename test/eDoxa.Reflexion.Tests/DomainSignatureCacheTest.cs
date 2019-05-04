// Filename: DomainSignatureCacheTest.cs
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

using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Reflexion.Tests
{
    [TestClass]
    public sealed class DomainSignatureCacheTest
    {
        [TestMethod]
        public void Count_NotEmptyCache_ShouldNotBeZero()
        {
            // Arrange
            var cache = new DomainSignatureCache();
            cache.GetOrAdd(typeof(MockBaseObject), type => new DomainSignature(type, Array.Empty<PropertyInfo>()));

            // Act
            var result = cache.Count;

            // Assert
            result.Should().NotBe(0);
        }

        [TestMethod]
        public void Clear_NotEmptyCache_ShouldBeZero()
        {
            // Arrange
            var cache = new DomainSignatureCache();
            cache.GetOrAdd(typeof(MockBaseObject), type => new DomainSignature(type, Array.Empty<PropertyInfo>()));

            // Act
            cache.Clear();

            // Assert
            cache.Count.Should().Be(0);
        }

        [TestMethod]
        public void Find_MatchingType_ShouldNotBeNull()
        {
            // Arrange
            var objectType = typeof(MockBaseObject);
            var cache = new DomainSignatureCache();
            cache.GetOrAdd(objectType, type => new DomainSignature(type, Array.Empty<PropertyInfo>()));

            // Act
            var signature = cache.Find(objectType);

            // Assert
            signature.Should().NotBeNull();
        }

        [TestMethod]
        public void Find_NotMatchingType_ShouldBeNull()
        {
            // Arrange
            var objectType = typeof(MockBaseObject);
            var cache = new DomainSignatureCache();

            // Act
            var signature = cache.Find(objectType);

            // Assert
            signature.Should().BeNull();
        }

        [TestMethod]
        public void GetOrAdd_KeyNotExist_ShouldBeSameSignature()
        {
            // Arrange
            var cache = new DomainSignatureCache();
            var objectType = typeof(MockBaseObject);
            var factory = new Func<Type, DomainSignature>(type => new DomainSignature(type, Array.Empty<PropertyInfo>()));
            var sameSignature = factory.Invoke(objectType);

            // Act
            var signature = cache.GetOrAdd(objectType, factory);

            // Assert
            signature.Should().Be(sameSignature);
            cache.Count.Should().Be(1);
        }

        [TestMethod]
        public void GetOrAdd_KeyExist_ShouldBeConcurrentSignature()
        {
            // Arrange
            var cache = new DomainSignatureCache();
            var objectType = typeof(MockBaseObject);
            var factory = new Func<Type, DomainSignature>(type => new DomainSignature(type, new PropertyInfo[5]));
            var concurrentSignature = factory.Invoke(objectType);
            cache.GetOrAdd(objectType, type => new DomainSignature(type, Array.Empty<PropertyInfo>()));

            // Act
            var signature = cache.GetOrAdd(objectType, factory);

            // Assert            
            signature.Should().Be(concurrentSignature);
            cache.Count.Should().Be(1);
        }

        private sealed class MockBaseObject : BaseObject
        {
            protected override PropertyInfo[] TypeSignatureProperties()
            {
                return Array.Empty<PropertyInfo>();
            }
        }
    }
}