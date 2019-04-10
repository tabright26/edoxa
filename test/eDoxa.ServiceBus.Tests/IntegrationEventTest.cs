// Filename: IntegrationEventTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.ServiceBus.Tests.Mocks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.ServiceBus.Tests
{
    [TestClass]
    public sealed class IntegrationEventTest
    {
        private MockIntegrationEvent1 _integrationEvent;

        [TestInitialize]
        public void TestInitialize()
        {
            // Arrange
            _integrationEvent = new MockIntegrationEvent1();
        }

        [TestMethod]
        public void IntegrationEvent_InitializedDefaultInstance_ShouldBeValid()
        {
            // Arrange => Act
            var integrationEvent = new MockIntegrationEvent1();

            // Assert
            Assert.AreNotEqual(Guid.Empty, integrationEvent.Id);
            integrationEvent.Created.Should().BeCloseTo(DateTime.UtcNow);
        }

        [TestMethod]
        [Description("The integration event is equal to its own prototype.")]
        public void EqualsOperator_IntegrationEventIsEqualToItsOwnPrototype_ShouldBeTrue()
        {
            // Arrange
            var integrationEvent = _integrationEvent.Clone();

            // Act
            var condition = _integrationEvent == integrationEvent;

            // Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        [Description("The integration event is equal to its own prototype.")]
        public void NotEqualsOperator_IntegrationEventIsEqualToItsOwnPrototype_ShouldBeFalse()
        {
            // Arrange
            var integrationEvent = _integrationEvent.Clone();

            // Act
            var condition = _integrationEvent != integrationEvent;

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        [Description("The integration event isn't equal to null reference.")]
        public void EqualsIntegrationEvent_IsEqualToNullReference_ShouldBeFalse()
        {
            // Act
            var condition = _integrationEvent.Equals(null);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        [Description("The integration event is equal to itself by memory reference.")]
        public void EqualsEntity_IntegrationEventIsEqualToItSelf_ShouldBeTrue()
        {
            // Act
            var condition = _integrationEvent.Equals(_integrationEvent);

            // Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        [Description("Two different types of entities are not equal.")]
        public void EqualsEntity_TwoDifferentTypesOfEntitiesAreNotEqual_ShouldBeTrue()
        {
            // Act
            var condition = !_integrationEvent.Equals(new MockIntegrationEvent2());

            // Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        [Description("The integration event is equal to its own prototype.")]
        public void EqualsIntegrationEvent_IntegrationEventIsEqualToItsOwnPrototype_ShouldBeTrue()
        {
            // Arrange
            var integrationEvent = _integrationEvent.Clone();

            // Act
            var condition = _integrationEvent.Equals(integrationEvent);

            // Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        [Description("The integration event is equal to the object that is a non-cast entity.")]
        public void EqualsObject_IntegrationEventIsEqualToTheObjectThatIsNonCastEntity_ShouldBeTrue()
        {
            // Arrange
            var obj = _integrationEvent.Clone() as object;

            // Act
            var condition = _integrationEvent.Equals(obj);

            // Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        [Description("The persistent integration event hash code is equal to its own prototype hash code.")]
        public void GetHashCode_IntegrationEventHashCodeIsEqualToItsOwnPrototypeHashCode_ShouldBeEqual()
        {
            // Arrange
            var integrationEventClone = _integrationEvent.Clone();

            var integrationEventCloneHashCode = integrationEventClone?.GetHashCode();

            // Act
            var integrationEventHashCode = _integrationEvent.GetHashCode();

            // Assert
            Assert.IsNotNull(integrationEventClone);

            Assert.AreEqual(integrationEventHashCode, (int) integrationEventCloneHashCode);
        }
    }
}