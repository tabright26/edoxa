// Filename: RabbitMqEventBusServiceTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Reflection;

using Autofac;

using eDoxa.ServiceBus.RabbitMQ;
using eDoxa.ServiceBus.Tests.Mocks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using RabbitMQ.Client;

namespace eDoxa.ServiceBus.Tests.RabbitMQ
{
    [TestClass]
    public sealed class RabbitMqEventBusServiceTest
    {
        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void Publish_PublishedAnIntegrationEvent_ShouldBeValid()
        {
            // Arrange
            var integrationEvent = new MockIntegrationEvent1();
            var eventBusService = GetEventBusService();

            // Act => Assert
            eventBusService.Publish(integrationEvent);
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void Subscribe()
        {
            // Arrange
            var eventBusService = GetEventBusService();

            // Act
            eventBusService.Subscribe<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Assert
            var handler =
                typeof(RabbitMqEventBusService).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eventBusService) as
                    InMemorySubscriptionHandler;

            Assert.IsNotNull(handler);
            Assert.IsTrue(handler.ContainsIntegrationEvent<MockIntegrationEvent1>());
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void SubscribeDynamic()
        {
            // Arrange
            var eventBusService = GetEventBusService();

            // Act
            eventBusService.SubscribeDynamic<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            var handler =
                typeof(RabbitMqEventBusService).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eventBusService) as
                    InMemorySubscriptionHandler;

            Assert.IsNotNull(handler);
            Assert.IsTrue(handler.ContainsIntegrationEvent(nameof(MockIntegrationEvent1)));
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void Unsubscribe()
        {
            // Arrange
            var eventBusService = GetEventBusService();
            eventBusService.Subscribe<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Act
            eventBusService.Unsubscribe<MockIntegrationEvent1, MockIntegrationEventHandler1>();

            // Assert
            var handler =
                typeof(RabbitMqEventBusService).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eventBusService) as
                    InMemorySubscriptionHandler;

            Assert.IsNotNull(handler);
            Assert.IsTrue(handler.IsEmpty);
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void UnsubscribeDynamic()
        {
            // Arrange
            var eventBusService = GetEventBusService();

            eventBusService.SubscribeDynamic<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Act
            eventBusService.UnsubscribeDynamic<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            var handler =
                typeof(RabbitMqEventBusService).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eventBusService) as
                    InMemorySubscriptionHandler;

            Assert.IsNotNull(handler);
            Assert.IsTrue(handler.IsEmpty);
        }

        private static IEventBusService GetEventBusService()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IRabbitMqPersistentConnection>(
                serviceProvider =>
                {
                    var logger = new Mock<ILogger<RabbitMqPersistentConnection>>();

                    var factory = new ConnectionFactory
                    {
                        HostName = "localhost",
                        Port = 5672,
                        UserName = "admin",
                        Password = "4t8SaVXVMy0rT2kM"
                    };

                    return new RabbitMqPersistentConnection(factory, logger.Object, 1);
                }
            );

            services.AddSingleton<IEventBusService, RabbitMqEventBusService>(
                serviceProvider =>
                {
                    var logger = new Mock<ILogger<RabbitMqEventBusService>>();

                    var scope = new Mock<ILifetimeScope>();

                    var connection = serviceProvider.GetRequiredService<IRabbitMqPersistentConnection>();

                    var handler = serviceProvider.GetRequiredService<ISubscriptionHandler>();

                    return new RabbitMqEventBusService(
                        logger.Object,
                        scope.Object,
                        connection,
                        handler,
                        1,
                        nameof(RabbitMqEventBusService)
                    );
                }
            );

            services.AddSingleton<ISubscriptionHandler, InMemorySubscriptionHandler>();

            var provider = services.BuildServiceProvider();

            return provider.GetService<IEventBusService>();
        }
    }
}
