// Filename: IntegrationEventTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.UnitTests.IntegrationEvents.Mocks;

using FluentAssertions;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents
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
        public void EqualsIntegrationEvent_IsEqualToNullReference_ShouldBeFalse()
        {
            // Act
            var condition = _integrationEvent.Equals(null);

            // Assert
            Assert.IsFalse(condition);
        }

        [TestMethod]
        public void EqualsEntity_IntegrationEventIsEqualToItSelf_ShouldBeTrue()
        {
            // Act
            var condition = _integrationEvent.Equals(_integrationEvent);

            // Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
        public void EqualsEntity_TwoDifferentTypesOfEntitiesAreNotEqual_ShouldBeTrue()
        {
            // Act
            var condition = !_integrationEvent.Equals(new MockIntegrationEvent2());

            // Assert
            Assert.IsTrue(condition);
        }

        [TestMethod]
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
