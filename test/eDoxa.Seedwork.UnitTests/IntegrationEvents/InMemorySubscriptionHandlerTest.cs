// Filename: InMemorySubscriptionHandlerTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;
using System.Linq;

using eDoxa.Seedwork.IntegrationEvents;
using eDoxa.Seedwork.IntegrationEvents.Exceptions;
using eDoxa.Seedwork.UnitTests.IntegrationEvents.Mocks;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents
{
    [TestClass]
    public sealed class InMemorySubscriptionHandlerTest
    {
        [TestMethod]
        public void OnIntegrationEventRemoved_TwoSubscriptionsAreAddedAndRemoved_ShouldBeRaisedExactlyTwoTimes()
        {
            // Arrange
            var onIntegrationEventRemovedEventRaisedCount = 0;
            var subscriptionHandler = new InMemorySubscriptionHandler();

            subscriptionHandler.OnIntegrationEventRemoved += (sender, eventArgs) => onIntegrationEventRemovedEventRaisedCount++;

            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            subscriptionHandler.AddDynamicSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent2));

            // Act
            subscriptionHandler.RemoveSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            subscriptionHandler.RemoveDynamicSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent2));

            // Assert
            Assert.AreEqual(2, onIntegrationEventRemovedEventRaisedCount);
        }

        [TestMethod]
        public void Property_IsEmpty_ShouldBeTrue()
        {
            // Arrange => Act
            var subscriptionHandler = new InMemorySubscriptionHandler();

            // Assert
            Assert.IsTrue(subscriptionHandler.IsEmpty);
        }

        [TestMethod]
        public void AddSubscription_SubscriptionHandlerIsEmpty_ShouldBeFalse()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();

            // Act
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Assert
            Assert.IsFalse(subscriptionHandler.IsEmpty);
        }

        [TestMethod]
        public void AddSubscription_SubscribedToTheSameIntegrationEvent_ShouldThrowIntegrationEventException()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act => Assert
            var exception = Assert.ThrowsException<SubscriptionException>(
                () => subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>()
            );

            Assert.IsInstanceOfType(exception.InnerException, typeof(ArgumentException));
        }

        [TestMethod]
        public void AddDynamicSubscription_SubscriptionHandlerIsEmpty_ShouldBeFalse()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();

            // Act
            subscriptionHandler.AddDynamicSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            Assert.IsFalse(subscriptionHandler.IsEmpty);
        }

        [TestMethod]
        public void AddDynamicSubscription_SubscribedToTheSameIntegrationEvent_ShouldThrowIntegrationEventException()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();

            subscriptionHandler.AddDynamicSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Act => Assert
            var exception = Assert.ThrowsException<SubscriptionException>(
                () => subscriptionHandler.AddDynamicSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1))
            );

            Assert.IsInstanceOfType(exception.InnerException, typeof(ArgumentException));
        }

        [TestMethod]
        public void ClearSubscriptions_AllIntegrationEventAreRemovedFromTheSubscriptionHandler_ShouldBeTrue()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act
            subscriptionHandler.ClearSubscriptions();

            // Assert
            Assert.IsTrue(subscriptionHandler.IsEmpty);
        }

        [TestMethod]
        public void ContainsIntegrationEvent_AddedIntegrationEventIsContainedByTheSubscriptionHandler_ShouldBeTrue()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act
            var condition1 = subscriptionHandler.ContainsIntegrationEvent(nameof(MockIntegrationEvent1));
            var condition2 = subscriptionHandler.ContainsIntegrationEvent<MockIntegrationEvent1>();

            // Assert
            Assert.IsTrue(condition1 && condition2);
        }

        [TestMethod]
        public void FindSubscription_FindTheAddedSubscription_ShouldBeEqualToTheIntegrationEventHandler()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act            
            var subscription1 = subscriptionHandler.FindSubscription(nameof(MockIntegrationEvent1), typeof(MockIntegrationEventHandler1));

            var subscription2 = subscriptionHandler.FindSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Assert
            Assert.AreEqual(typeof(MockIntegrationEventHandler1), subscription1.IntegrationEventHandlerType);
            Assert.IsFalse(subscription1.IsDynamic);
            Assert.IsTrue(subscription1.Equals(subscription2));
        }

        [TestMethod]
        public void FindDynamicSubscription_FindTheAddedDynamicSubscription_ShouldBeEqualToTheDynamicIntegrationEventHandler()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();

            subscriptionHandler.AddDynamicSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Act            
            var subscription = subscriptionHandler.FindDynamicSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            Assert.AreEqual(typeof(MockDynamicIntegrationEventHandler1), subscription.IntegrationEventHandlerType);
            Assert.IsTrue(subscription.IsDynamic);
        }

        [TestMethod]
        public void FindAllSubscriptions_TwoSubscriptionsAreAddedAndTheMethodsAreCalledBoth_ShouldBeCountOfFour()
        {
            // Arrange
            var subscriptions = new List<Subscription>();
            var subscriptionHandler = new InMemorySubscriptionHandler();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            subscriptionHandler.AddDynamicSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent1));

            // Act            
            subscriptions.AddRange(subscriptionHandler.FindAllSubscriptions<MockIntegrationEvent1>());
            subscriptions.AddRange(subscriptionHandler.FindAllSubscriptions(nameof(MockIntegrationEvent1)));

            // Assert
            Assert.AreEqual(4, subscriptions.Count());
        }

        [TestMethod]
        public void GetIntegrationEventKey_KeyOfSubscriptionAdded_ShouldBeKeyOfIntegrationEvent()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();

            // Act
            var integrationEventKey = subscriptionHandler.GetIntegrationEventKey<MockIntegrationEvent1>();

            // Assert
            Assert.AreEqual(nameof(MockIntegrationEvent1), integrationEventKey);
        }

        [TestMethod]
        public void GetIntegrationEventType_TypeOfSubscriptionAdded_ShouldBeTypeOfIntegrationEvent()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act
            var integrationEventType = subscriptionHandler.GetIntegrationEventType(nameof(MockIntegrationEvent1));

            // Assert
            Assert.AreEqual(typeof(MockIntegrationEvent1), integrationEventType);
        }

        [TestMethod]
        public void RemoveSubscription_ContainsIntegrationEvent_ShouldBeFalse()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act
            subscriptionHandler.RemoveSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Assert
            Assert.IsFalse(subscriptionHandler.ContainsIntegrationEvent<MockIntegrationEvent1>());
        }

        [TestMethod]
        public void RemoveSubscription_OnIntegrationEventRemovedEvent_ShouldNotBeRaised()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();
            var onIntegrationEventRemovedEventRaisedCount = 0;

            subscriptionHandler.OnIntegrationEventRemoved += (sender, eventArgs) => onIntegrationEventRemovedEventRaisedCount++;

            // Act
            subscriptionHandler.RemoveSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Arrange
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler2>();

            // Act
            subscriptionHandler.RemoveSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Assert
            Assert.AreEqual(0, onIntegrationEventRemovedEventRaisedCount);
        }

        [TestMethod]
        public void RemoveDynamicSubscription_ContainsIntegrationEvent_ShouldBeFalse()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();

            subscriptionHandler.AddDynamicSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Act            
            subscriptionHandler.RemoveDynamicSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            Assert.IsFalse(subscriptionHandler.ContainsIntegrationEvent<MockIntegrationEvent1>());
        }

        [TestMethod]
        public void RemoveDynamicSubscription_OnIntegrationEventRemovedEvent_ShouldNotBeRaised()
        {
            // Arrange
            var subscriptionHandler = new InMemorySubscriptionHandler();
            var onIntegrationEventRemovedEventRaisedCount = 0;

            subscriptionHandler.OnIntegrationEventRemoved += (sender, eventArgs) => onIntegrationEventRemovedEventRaisedCount++;

            // Act
            subscriptionHandler.RemoveDynamicSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent2));

            // Arrange
            subscriptionHandler.AddDynamicSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            subscriptionHandler.AddDynamicSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent1));

            // Act
            subscriptionHandler.RemoveDynamicSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            Assert.AreEqual(0, onIntegrationEventRemovedEventRaisedCount);
        }
    }
}
