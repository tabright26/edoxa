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

        private class MockEntityId : EntityId<MockEntityId>
        {
        }
    }
}