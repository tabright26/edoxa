// Filename: EntityTest.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Aggregate;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.Aggregate
{
    [TestClass]
    public sealed class EntityTest
    {
        [TestMethod]
        public void OperatorEquals_SameEntityId_ShouldBeTrue()
        {
            // Arrange
            var entityId = new MockEntityId();

            var entity1 = new MockEntity(entityId);

            var entity2 = new MockEntity(entityId);

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
            var entity1 = new MockEntity(entityId1);

            var entityId2 = new MockEntityId();
            var entity2 = new MockEntity(entityId2);

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
            var entity1 = new MockEntity(entityId1);

            var entityId2 = new MockEntityId();
            var entity2 = new MockEntity(entityId2);

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
            var entity1 = new MockEntity(entityId);
            var entity2 = new MockEntity(entityId);

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
            var entity1 = new MockEntity(entityId);
            var entity2 = new MockEntity(entityId);

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
            var entity1 = new MockEntity(entityId1);

            var entityId2 = new MockEntityId();
            var entity2 = new MockEntity(entityId2);

            // Act
            var condition = entity1.Equals(entity2);

            // Assert
            condition.Should().BeFalse();
        }

        [TestMethod]
        public void Equals_DifferentTypes_ShouldBeFalse()
        {
            // Arrange
            var entityId = new MockEntityId();
            var entity1 = new MockEntity(entityId);
            var entity2 = new MockEntityWithDomainEvents(entityId);

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
            var entity1 = new MockEntity(entityId);
            var entity2 = new MockEntity(entityId);

            // Act
            var hashCode1 = entity1.GetHashCode();
            var hashCode2 = entity2.GetHashCode();

            // Assert
            hashCode1.Should().Be(hashCode2);
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
            public MockEntityWithDomainEvents(MockEntityId mockEntityId)
            {
                this.SetEntityId(mockEntityId);
                this.AddDomainEvent(new MockDomainEvent());
                this.AddDomainEvent(new MockDomainEvent());
            }

            public MockEntityWithDomainEvents()
            {
            }
        }

        private class MockEntity : Entity<MockEntityId>, IAggregateRoot
        {
            public MockEntity(MockEntityId mockEntityId)
            {
                this.SetEntityId(mockEntityId);
            }

            public MockEntity()
            {
            }
        }

        private sealed class MockEntityId : EntityId<MockEntityId>
        {
        }
    }
}
