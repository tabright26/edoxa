// Filename: EntityTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.Domain.Aggregate;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.Domain.Tests.Aggregate
{
    [TestClass]
    public sealed class EntityTest
    {
        [TestMethod]
        public void Id_NullReference_ShouldThrowArgumentNullException()
        {
            // Arrange
            var entity = new MockEntity();

            // Act
            var action = new Action(() => entity.SetEntityIdProperty(null));

            // Assert
            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void OperatorEquals_SameEntityId_ShouldBeTrue()
        {
            // Arrange
            var entityId = new MockEntityId();

            var entity1 = new MockEntity();
            entity1.SetEntityIdProperty(entityId);

            var entity2 = new MockEntity();
            entity2.SetEntityIdProperty(entityId);

            // Act
            var condition = entity1 == entity2;

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void OperatorEquals_DifferentEntityId_ShouldBeFalse()
        {
            // Arrange
            var entityId1 = new MockEntityId();
            var entity1 = new MockEntity();
            entity1.SetEntityIdProperty(entityId1);

            var entityId2 = new MockEntityId();
            var entity2 = new MockEntity();
            entity2.SetEntityIdProperty(entityId2);

            // Act
            var condition = entity1 == entity2;

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void OperatorNotEquals_DifferentEntityId_ShouldBeTrue()
        {
            // Arrange
            var entityId1 = new MockEntityId();
            var entity1 = new MockEntity();
            entity1.SetEntityIdProperty(entityId1);

            var entityId2 = new MockEntityId();
            var entity2 = new MockEntity();
            entity2.SetEntityIdProperty(entityId2);

            // Act
            var condition = entity1 != entity2;

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void OperatorNotEquals_SameEntityId_ShouldBeFalse()
        {
            // Arrange
            var entityId = new MockEntityId();

            var entity1 = new MockEntity();
            entity1.SetEntityIdProperty(entityId);

            var entity2 = new MockEntity();
            entity2.SetEntityIdProperty(entityId);

            // Act
            var condition = entity1 != entity2;

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_SameEntityId_ShouldBeTrue()
        {
            // Arrange
            var entityId = new MockEntityId();

            var entity1 = new MockEntity();
            entity1.SetEntityIdProperty(entityId);

            var entity2 = new MockEntity();
            entity2.SetEntityIdProperty(entityId);

            // Act
            var condition = entity1.Equals(entity2);

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_DifferentEntityId_ShouldBeFalse()
        {
            // Arrange
            var entityId1 = new MockEntityId();
            var entity1 = new MockEntity();
            entity1.SetEntityIdProperty(entityId1);

            var entityId2 = new MockEntityId();
            var entity2 = new MockEntity();
            entity2.SetEntityIdProperty(entityId2);

            // Act
            var condition = entity1.Equals(entity2);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_NullReference_ShouldBeFalse()
        {
            // Arrange
            var entity = new MockEntity();

            // Act
            var condition = entity.Equals(null);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_ReferenceEquals_ShouldBeTrue()
        {
            // Arrange
            var entity = new MockEntity();

            // Act
            var condition = entity.Equals(entity);

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void Equals_DifferentTypes_ShouldBeFalse()
        {
            // Arrange
            var entityId = new MockEntityId();
            var entity1 = new MockEntity();
            entity1.SetEntityIdProperty(entityId);
            var entity2 = new MockEntityWithDomainEvents();
            entity2.SetEntityIdProperty(entityId);

            // Act
            var condition = entity1.Equals(entity2);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_IsTransient_ShouldBeFalse()
        {
            // Arrange            
            var entity1 = new MockEntity();

            var entityId = new MockEntityId();
            var entity2 = new MockEntity();
            entityId.SetPrivateField("_value", Guid.Empty);
            entity2.SetEntityIdProperty(entityId);

            // Act
            var condition = entity1.Equals(entity2);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void GetHashCode_SameEntityId_ShouldBeEquals()
        {
            // Arrange
            var entityId = new MockEntityId();
            var entity1 = new MockEntity();
            entity1.SetEntityIdProperty(entityId);
            var entity2 = new MockEntity();
            entity2.SetEntityIdProperty(entityId);

            // Act
            var hashCode1 = entity1.GetHashCode();
            var hashCode2 = entity2.GetHashCode();

            // Assert
            hashCode1.Should().Be(hashCode2);
        }

        [TestMethod]
        public void GetHashCode_CachedHashCodeHasValue_ShouldBeCachedHashCode()
        {
            // Arrange
            var cachedHashCode = -54798654;
            var entity = new MockEntity();
            entity.SetPrivateField("_cachedHashCode", cachedHashCode);

            // Act
            var hashCode = entity.GetHashCode();

            // Assert
            hashCode.Should().Be(cachedHashCode);
        }

        [TestMethod]
        public void GetHashCode_IsTransient_ShouldAssignBaseHashCodeToCachedHashCode()
        {
            // Arrange
            var entity = new MockEntity();
            entity.SetPrivateField("_cachedHashCode", null);
            entity.SetPrivateField("_id", null);

            // Act
            var hashCode = entity.GetHashCode();

            // Assert
            entity.GetPrivateField("_cachedHashCode").Should().Be(hashCode);
        }

        [TestMethod]
        public void IsTransient_NullEntityId_ShouldBeTrue()
        {
            // Arrange            
            var entity = new MockEntity();
            entity.SetPrivateField("_id", null);

            // Act
            var condition = entity.IsTransient();

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void IsTransient_NotNullEntityId_ShouldBeFalse()
        {
            // Arrange
            var entity = new MockEntity();

            // Act
            var condition = entity.IsTransient();

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void IsTransient_DefaultEntityId_ShouldBeTrue()
        {
            // Arrange            
            var entityId = new MockEntityId();
            entityId.SetPrivateField("_value", Guid.Empty);
            var entity = new MockEntity();
            entity.SetEntityIdProperty(entityId);

            // Act
            var condition = entity.IsTransient();

            // Assert
            condition.Should().BeTrue();
        }

        [TestMethod]
        public void AddDomainEvent_SingleDomainEvent_ShouldNotBeEmpty()
        {
            // Arrange
            var entity = new MockEntity();
            var domainEvent = new MockDomainEvent();

            // Act
            entity.AddDomainEvent(domainEvent);

            // Assert
            entity.DomainEvents.Should().NotBeEmpty();
        }

        [TestMethod]
        public void AddDomainEvent_NullReference_ShouldThrowArgumentNullException()
        {
            // Arrange
            var entity = new MockEntity();

            // Act
            var action = new Action(() => entity.AddDomainEvent(null));

            // Assert
            Assert.ThrowsException<ArgumentNullException>(action);
        }

        [TestMethod]
        public void ClearDomainEvents_EmptyList_ShouldBeEmpty()
        {
            // Arrange
            var entity = new MockEntityWithDomainEvents();

            // Act
            entity.ClearDomainEvents();

            // Assert
            entity.DomainEvents.Should().BeEmpty();
        }

        private sealed class MockDomainEvent : IDomainEvent
        {
        }

        private sealed class MockEntityWithDomainEvents : MockEntity
        {
            public MockEntityWithDomainEvents()
            {
                this.AddDomainEvent(new MockDomainEvent());
                this.AddDomainEvent(new MockDomainEvent());
            }
        }

        private class MockEntity : Entity<MockEntityId>, IAggregateRoot
        {
        }

        private sealed class MockEntityId : EntityId<MockEntityId>
        {
        }
    }
}