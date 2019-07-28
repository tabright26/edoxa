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

using eDoxa.Seedwork.IntegrationEvents;
using eDoxa.Seedwork.IntegrationEvents.Infrastructure;
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
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();

            subscriptionHandler.OnIntegrationEventRemoved += (sender, eventArgs) => onIntegrationEventRemovedEventRaisedCount++;

            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            subscriptionHandler.AddSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent2));

            // Act
            subscriptionHandler.RemoveSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            subscriptionHandler.RemoveSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent2));

            // Assert
            Assert.AreEqual(2, onIntegrationEventRemovedEventRaisedCount);
        }

        [TestMethod]
        public void Property_IsEmpty_ShouldBeTrue()
        {
            // Arrange => Act
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();

            // Assert
            Assert.IsTrue(subscriptionHandler.IsEmpty);
        }

        [TestMethod]
        public void AddSubscription_SubscriptionHandlerIsEmpty_ShouldBeFalse()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();

            // Act
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Assert
            Assert.IsFalse(subscriptionHandler.IsEmpty);
        }

        [TestMethod]
        public void AddSubscription_SubscribedToTheSameIntegrationEvent_ShouldThrowIntegrationEventException()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act => Assert
            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>()
            );
        }

        [TestMethod]
        public void AddDynamicSubscription_SubscriptionHandlerIsEmpty_ShouldBeFalse()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();

            // Act
            subscriptionHandler.AddSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            Assert.IsFalse(subscriptionHandler.IsEmpty);
        }

        [TestMethod]
        public void AddDynamicSubscription_SubscribedToTheSameIntegrationEvent_ShouldThrowIntegrationEventException()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();

            subscriptionHandler.AddSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Act => Assert
            var exception = Assert.ThrowsException<InvalidOperationException>(
                () => subscriptionHandler.AddSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1))
            );
        }

        [TestMethod]
        public void ClearSubscriptions_AllIntegrationEventAreRemovedFromTheSubscriptionHandler_ShouldBeTrue()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act
            subscriptionHandler.Clear();

            // Assert
            Assert.IsTrue(subscriptionHandler.IsEmpty);
        }

        [TestMethod]
        public void ContainsIntegrationEvent_AddedIntegrationEventIsContainedByTheSubscriptionHandler_ShouldBeTrue()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act
            var condition1 = subscriptionHandler.Contains(nameof(MockIntegrationEvent1));
            var condition2 = subscriptionHandler.Contains<MockIntegrationEvent1>();

            // Assert
            Assert.IsTrue(condition1 && condition2);
        }

        //[TestMethod]
        //public void FindSubscription_FindTheAddedSubscription_ShouldBeEqualToTheIntegrationEventHandler()
        //{
        //    // Arrange
        //    var subscriptionHandler = new InMemorySubscriptionCollection();
        //    subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

        //    // Act            
        //    var subscription1 = subscriptionHandler.TryGetSubscription(nameof(MockIntegrationEvent1), typeof(MockIntegrationEventHandler1));

        //    var subscription2 = subscriptionHandler.TryGetSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

        //    // Assert
        //    Assert.AreEqual(typeof(MockIntegrationEventHandler1), subscription1.IntegrationEventHandlerType);
        //    Assert.IsFalse(subscription1.IsDynamic);
        //    Assert.IsTrue(subscription1.Equals(subscription2));
        //}

        //[TestMethod]
        //public void FindDynamicSubscription_FindTheAddedDynamicSubscription_ShouldBeEqualToTheDynamicIntegrationEventHandler()
        //{
        //    // Arrange
        //    var subscriptionHandler = new InMemorySubscriptionCollection();

        //    subscriptionHandler.AddSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

        //    // Act            
        //    var subscription = subscriptionHandler.TryGetSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

        //    // Assert
        //    Assert.AreEqual(typeof(MockDynamicIntegrationEventHandler1), subscription.IntegrationEventHandlerType);
        //    Assert.IsTrue(subscription.IsDynamic);
        //}

        [TestMethod]
        public void FindAllSubscriptions_TwoSubscriptionsAreAddedAndTheMethodsAreCalledBoth_ShouldBeCountOfFour()
        {
            // Arrange
            var subscriptions = new List<IntegrationEventSubscription>();
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            subscriptionHandler.AddSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent1));

            // Act            
            subscriptions.AddRange(subscriptionHandler.FetchSubscriptions<MockIntegrationEvent1>());
            subscriptions.AddRange(subscriptionHandler.FetchSubscriptions(nameof(MockIntegrationEvent1)));

            // Assert
            Assert.AreEqual(4, subscriptions.Count);
        }

        [TestMethod]
        public void GetIntegrationEventType_TypeOfSubscriptionAdded_ShouldBeTypeOfIntegrationEvent()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act
            var integrationEventType = subscriptionHandler.TryGetIntegrationEventType(nameof(MockIntegrationEvent1));

            // Assert
            Assert.AreEqual(typeof(MockIntegrationEvent1), integrationEventType);
        }

        [TestMethod]
        public void RemoveSubscription_ContainsIntegrationEvent_ShouldBeFalse()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();
            subscriptionHandler.AddSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act
            subscriptionHandler.RemoveSubscription<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Assert
            Assert.IsFalse(subscriptionHandler.Contains<MockIntegrationEvent1>());
        }

        [TestMethod]
        public void RemoveSubscription_OnIntegrationEventRemovedEvent_ShouldNotBeRaised()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();
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
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();

            subscriptionHandler.AddSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Act            
            subscriptionHandler.RemoveSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            Assert.IsFalse(subscriptionHandler.Contains<MockIntegrationEvent1>());
        }

        [TestMethod]
        public void RemoveDynamicSubscription_OnIntegrationEventRemovedEvent_ShouldNotBeRaised()
        {
            // Arrange
            var subscriptionHandler = new InMemoryIntegrationEventSubscriptionStore();
            var onIntegrationEventRemovedEventRaisedCount = 0;

            subscriptionHandler.OnIntegrationEventRemoved += (sender, eventArgs) => onIntegrationEventRemovedEventRaisedCount++;

            // Act
            subscriptionHandler.RemoveSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent2));

            // Arrange
            subscriptionHandler.AddSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            subscriptionHandler.AddSubscription<MockDynamicIntegrationEventHandler2>(nameof(MockIntegrationEvent1));

            // Act
            subscriptionHandler.RemoveSubscription<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            Assert.AreEqual(0, onIntegrationEventRemovedEventRaisedCount);
        }
    }
}
