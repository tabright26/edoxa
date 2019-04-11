﻿// Filename: SubscriptionTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.ServiceBus.Tests.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.ServiceBus.Tests
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
            var subscription = Subscription.FromIntegrationEventHandler(integrationEventHandlerType);

            // Assert            
            Assert.AreEqual(integrationEventHandlerType, subscription.IntegrationEventHandlerType);
            Assert.IsFalse(subscription.IsDynamic);
        }

        [TestMethod]
        public void
            FromDynamicIntegrationEventHandler_CreatedDynamicSubscriptionIntegrationEventHandlerType_ShouldBeEqualToTheDynamicIntegrationEventHandlerType()
        {
            // Arrange
            var dynamicIntegrationEventHandlerType = typeof(MockDynamicIntegrationEventHandler1);

            // Act
            var subscription = Subscription.FromDynamicIntegrationEventHandler(dynamicIntegrationEventHandlerType);

            // Assert            
            Assert.AreEqual(dynamicIntegrationEventHandlerType, subscription.IntegrationEventHandlerType);
            Assert.IsTrue(subscription.IsDynamic);
        }
    }
}