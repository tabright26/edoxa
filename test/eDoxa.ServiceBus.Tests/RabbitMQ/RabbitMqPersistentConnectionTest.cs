// Filename: RabbitMqPersistentConnectionTest.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.ServiceBus.Exceptions;
using eDoxa.ServiceBus.RabbitMQ;
using eDoxa.Testing.MSTest.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Logging.Internal;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;

namespace eDoxa.ServiceBus.Tests.RabbitMQ
{
    [TestClass]
    public sealed class RabbitMqPersistentConnectionTest
    {
        private const string HostName = "localhost";
        private const int Port = 5672;
        private const string UserName = "admin";
        private const string Password = "4t8SaVXVMy0rT2kM";

        [TestMethod]
        public void TryConnect_IsConnected_ShouldBeTrue()
        {
            // Arrange
            var factory = new ConnectionFactory
            {
                HostName = HostName, Port = Port, UserName = UserName, Password = Password
            };

            var mockLogger = new Mock<ILogger<RabbitMqPersistentConnection>>();

            mockLogger.SetupLoggerWithLogInformationVerifiable();

            using (var connection = new RabbitMqPersistentConnection(factory, mockLogger.Object))
            {
                // Act
                connection.TryConnect();

                // Assert
                Assert.IsTrue(connection.IsConnected);

                mockLogger.Verify(
                    logger => logger.Log(
                        LogLevel.Information,
                        It.IsAny<EventId>(),
                        It.IsAny<FormattedLogValues>(),
                        It.IsAny<Exception>(),
                        It.IsAny<Func<object, Exception, string>>()
                    ),
                    Times.Exactly(2)
                );
            }
        }

        [TestMethod]
        public void TryConnect_RetryPolicyTryConnectOnce_ShouldThrownBrokerUnreachableException()
        {
            // Arrange
            var factory = new ConnectionFactory();

            var mockLogger = new Mock<ILogger<RabbitMqPersistentConnection>>();

            mockLogger.SetupLoggerWithLogInformationVerifiable();

            var connection = new RabbitMqPersistentConnection(factory, mockLogger.Object, 1);

            // Act => Assert
            Assert.ThrowsException<BrokerUnreachableException>(() => connection.TryConnect());

            mockLogger.Verify(
                logger => logger.Log(
                    LogLevel.Warning,
                    It.IsAny<EventId>(),
                    It.IsAny<FormattedLogValues>(),
                    It.IsAny<Exception>(),
                    It.IsAny<Func<object, Exception, string>>()
                ),
                Times.Once()
            );
        }

        [TestMethod]
        public void CreateChannel_TryConnectAndCreateChannel_ShouldBeNotNull()
        {
            // Arrange
            var factory = new ConnectionFactory
            {
                HostName = HostName, Port = Port, UserName = UserName, Password = Password
            };

            var mockLogger = new Mock<ILogger<RabbitMqPersistentConnection>>();

            using (var connection = new RabbitMqPersistentConnection(factory, mockLogger.Object))
            {
                if (connection.TryConnect())
                {
                    // Act
                    var channel = connection.CreateChannel();

                    // Assert
                    Assert.IsNotNull(channel);
                }
                else
                {
                    throw new RabbitMqException("The Docker 'edoxa.rabbitmq.broker' container isn't running.");
                }
            }
        }

        [TestMethod]
        public void CreateChannel_CreateChannelWithoutConnection_ShouldThrownRabbitMqException()
        {
            // Arrange
            var factory = new ConnectionFactory
            {
                HostName = HostName, Port = Port, UserName = UserName, Password = Password
            };

            var mockLogger = new Mock<ILogger<RabbitMqPersistentConnection>>();

            var connection = new RabbitMqPersistentConnection(factory, mockLogger.Object);

            // Act => Assert
            Assert.ThrowsException<RabbitMqException>(() => connection.CreateChannel());
        }

        private static IRabbitMqPersistentConnection GetRabbitMqEventBusPersistentConnection()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IRabbitMqPersistentConnection>(
                serviceProvider =>
                {
                    var mockLogger = new Mock<ILogger<RabbitMqPersistentConnection>>();

                    var factory = new ConnectionFactory
                    {
                        HostName = HostName, Port = Port, UserName = UserName, Password = Password
                    };

                    return new RabbitMqPersistentConnection(factory, mockLogger.Object);
                }
            );

            var provider = services.BuildServiceProvider();

            return provider.GetService<IRabbitMqPersistentConnection>();
        }
    }
}