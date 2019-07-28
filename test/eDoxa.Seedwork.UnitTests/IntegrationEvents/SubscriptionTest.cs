// Filename: SubscriptionTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.IntegrationEvents;
using eDoxa.Seedwork.UnitTests.IntegrationEvents.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class SubscriptionTest
    {
        [TestMethod]
        public void FromIntegrationEventHandler_CreatedSubscriptionIntegrationEventHandlerType_ShouldBeEqualToTheIntegrationEventHandlerType()
        {
            // Arrange
            var integrationEventHandlerType = typeof(MockIntegrationEventHandler1);

            // Act
            var subscription = new IntegrationEventSubscription(integrationEventHandlerType);

            // Assert            
            Assert.AreEqual(integrationEventHandlerType, subscription.HandlerType);
            Assert.IsFalse(subscription.IsDynamic);
        }

        [TestMethod]
        public void
            FromDynamicIntegrationEventHandler_CreatedDynamicSubscriptionIntegrationEventHandlerType_ShouldBeEqualToTheDynamicIntegrationEventHandlerType()
        {
            // Arrange
            var dynamicIntegrationEventHandlerType = typeof(MockDynamicIntegrationEventHandler1);

            // Act
            var subscription = new DynamicIntegrationEventSubscription(dynamicIntegrationEventHandlerType);

            // Assert            
            Assert.AreEqual(dynamicIntegrationEventHandlerType, subscription.HandlerType);
            Assert.IsTrue(subscription.IsDynamic);
        }
    }
}
