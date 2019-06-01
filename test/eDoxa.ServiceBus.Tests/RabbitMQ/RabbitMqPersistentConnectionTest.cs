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

using eDoxa.ServiceBus.Exceptions;
using eDoxa.ServiceBus.RabbitMQ;
using eDoxa.Testing.MSTest.Extensions;

using FluentAssertions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace eDoxa.ServiceBus.Tests.RabbitMQ
{
    [TestClass]
    public sealed class RabbitMqPersistentConnectionTest
    {
        private static readonly ConnectionFactory ConnectionFactory = new ConnectionFactory
        {
            HostName = "localhost",
            Port = 5672,
            UserName = "admin",
            Password = "4t8SaVXVMy0rT2kM"
        };

        private Mock<ILogger<RabbitMqPersistentConnection>> _mockLogger;

        public RabbitMqPersistentConnectionTest()
        {
            _mockLogger = new Mock<ILogger<RabbitMqPersistentConnection>>();
            _mockLogger.SetupLog();
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void TryConnect_IsConnected_ShouldBeTrue()
        {
            using (var connection = new RabbitMqPersistentConnection(ConnectionFactory, _mockLogger.Object))
            {
                // Act
                connection.TryConnect();

                // Assert
                connection.IsConnected.Should().BeTrue();
            }
        }

        [TestMethod]
        public void TryConnect_RetryPolicyTryConnectOnce_ShouldThrownBrokerUnreachableException()
        {
            // Arrange
            var connection = new RabbitMqPersistentConnection(new ConnectionFactory(), _mockLogger.Object, 1);

            // Act
            var action = new Action(() => connection.TryConnect());

            // Assert
            action.Should().Throw<BrokerUnreachableException>();
        }

        [Ignore("Must be transferred to an integration test project because it requires external dependencies (RabbitMQ).")]
        public void CreateChannel_TryConnectAndCreateChannel_ShouldBeNotNull()
        {
            using (var connection = new RabbitMqPersistentConnection(ConnectionFactory, _mockLogger.Object))
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
            var connection = new RabbitMqPersistentConnection(ConnectionFactory, _mockLogger.Object);

            // Act
            var action = new Action(() => connection.CreateChannel());

            // Assert
            action.Should().Throw<RabbitMqException>();
        }

        private IRabbitMqPersistentConnection GetRabbitMqEventBusPersistentConnection()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IRabbitMqPersistentConnection>(serviceProvider => new RabbitMqPersistentConnection(ConnectionFactory, _mockLogger.Object));

            var provider = services.BuildServiceProvider();

            return provider.GetService<IRabbitMqPersistentConnection>();
        }
    }
}
