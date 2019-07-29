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

using eDoxa.Seedwork.ServiceBus;
using eDoxa.Seedwork.ServiceBus.RabbitMq;
using eDoxa.Seedwork.UnitTests.IntegrationEvents.Mocks;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using RabbitMQ.Client;

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents.RabbitMQ
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
                typeof(RabbitMqServiceBusPublisher).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eventBusService) as
                    InMemoryServiceBusStore;

            Assert.IsNotNull(handler);
            Assert.IsTrue(handler.Contains<MockIntegrationEvent1>());
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void SubscribeDynamic()
        {
            // Arrange
            var eventBusService = GetEventBusService();

            // Act
            eventBusService.Subscribe<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            var handler =
                typeof(RabbitMqServiceBusPublisher).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eventBusService) as
                    InMemoryServiceBusStore;

            Assert.IsNotNull(handler);
            Assert.IsTrue(handler.Contains(nameof(MockIntegrationEvent1)));
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
                typeof(RabbitMqServiceBusPublisher).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eventBusService) as
                    InMemoryServiceBusStore;

            Assert.IsNotNull(handler);
            Assert.IsTrue(handler.IsEmpty);
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void UnsubscribeDynamic()
        {
            // Arrange
            var eventBusService = GetEventBusService();

            eventBusService.Subscribe<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Act
            eventBusService.Unsubscribe<MockDynamicIntegrationEventHandler1>(nameof(MockIntegrationEvent1));

            // Assert
            var handler =
                typeof(RabbitMqServiceBusPublisher).GetField("_handler", BindingFlags.NonPublic | BindingFlags.Instance).GetValue(eventBusService) as
                    InMemoryServiceBusStore;

            Assert.IsNotNull(handler);
            Assert.IsTrue(handler.IsEmpty);
        }

        private static IServiceBusPublisher GetEventBusService()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IRabbitMqServiceBusContext>(
                serviceProvider =>
                {
                    var logger = new Mock<ILogger<RabbitMqServiceBusContext>>();

                    var factory = new ConnectionFactory
                    {
                        HostName = "localhost",
                        Port = 5672,
                        UserName = "admin",
                        Password = "4t8SaVXVMy0rT2kM"
                    };

                    return new RabbitMqServiceBusContext(factory, logger.Object, 1);
                }
            );

            services.AddSingleton<IServiceBusPublisher, RabbitMqServiceBusPublisher>(
                serviceProvider =>
                {
                    var logger = new Mock<ILogger<RabbitMqServiceBusPublisher>>();

                    var scope = new Mock<ILifetimeScope>();

                    var connection = serviceProvider.GetRequiredService<IRabbitMqServiceBusContext>();

                    var handler = serviceProvider.GetRequiredService<IServiceBusStore>();

                    return new RabbitMqServiceBusPublisher(
                        logger.Object,
                        scope.Object,
                        connection,
                        handler,
                        1,
                        nameof(RabbitMqServiceBusPublisher)
                    );
                }
            );

            services.AddSingleton<IServiceBusStore, InMemoryServiceBusStore>();

            var provider = services.BuildServiceProvider();

            return provider.GetService<IServiceBusPublisher>();
        }
    }
}
