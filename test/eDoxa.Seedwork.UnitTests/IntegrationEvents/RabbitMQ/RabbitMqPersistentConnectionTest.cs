// Filename: RabbitMqPersistentConnectionTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Seedwork.IntegrationEvents.RabbitMq;
using eDoxa.Seedwork.Testing.Helpers.Mocks;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents.RabbitMQ
{
    [TestClass]
    public sealed class RabbitMqPersistentConnectionTest
    {
        private MockLogger<RabbitMqServiceBusContext> _mockLogger;

        public RabbitMqPersistentConnectionTest()
        {
            _mockLogger = new MockLogger<RabbitMqServiceBusContext>();
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void TryConnect_IsConnected_ShouldBeTrue()
        {
            using (var connection = new RabbitMqServiceBusContext(new ConnectionFactory(), _mockLogger.Object))
            {
                // Act
                connection.TryConnect();

                // Assert
                connection.IsConnected.Should().BeTrue();
            }
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void TryConnect_RetryPolicyTryConnectOnce_ShouldThrownBrokerUnreachableException()
        {
            // Arrange
            var connection = new RabbitMqServiceBusContext(new ConnectionFactory(), _mockLogger.Object, 1);

            // Act
            var action = new Action(() => connection.TryConnect());

            // Assert
            action.Should().Throw<BrokerUnreachableException>();
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void CreateChannel_TryConnectAndCreateChannel_ShouldBeNotNull()
        {
            using (var connection = new RabbitMqServiceBusContext(new ConnectionFactory(), _mockLogger.Object))
            {
                // Arrange
                connection.TryConnect();

                // Act
                var channel = connection.CreateChannel();

                // Assert
                channel.Should().NotBeNull();
            }
        }

        [TestMethod]
        public void CreateChannel_CreateChannelWithoutConnection_ShouldThrownRabbitMqException()
        {
            // Arrange
            var connection = new RabbitMqServiceBusContext(new ConnectionFactory(), _mockLogger.Object);

            // Act
            var action = new Action(() => connection.CreateChannel());

            // Assert
            action.Should().Throw<InvalidOperationException>();
        }

        private IRabbitMqServiceBusContext GetRabbitMqEventBusPersistentConnection()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IRabbitMqServiceBusContext>(serviceProvider => new RabbitMqServiceBusContext(new ConnectionFactory(), _mockLogger.Object));

            var provider = services.BuildServiceProvider();

            return provider.GetService<IRabbitMqServiceBusContext>();
        }
    }
}
