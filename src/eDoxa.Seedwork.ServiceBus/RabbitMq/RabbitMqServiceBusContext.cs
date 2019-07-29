// Filename: RabbitMqServiceBusContext.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.IO;
using System.Net.Sockets;

using Microsoft.Extensions.Logging;

using Polly;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace eDoxa.Seedwork.ServiceBus.RabbitMq
{
    public sealed class RabbitMqServiceBusContext : IRabbitMqServiceBusContext
    {
        private static readonly object SyncRoot = new object();

        private readonly int _retryCount;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqServiceBusContext> _logger;

        private IConnection _connection;

        private bool _disposed;

        public RabbitMqServiceBusContext(IConnectionFactory connectionFactory, ILogger<RabbitMqServiceBusContext> logger, int retryCount = 5)
        {
            _connectionFactory = connectionFactory;
            _logger = logger;
            _retryCount = retryCount;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;

            try
            {
                _connection.Dispose();
            }
            catch (IOException exception)
            {
                _logger.LogCritical(exception.ToString());
            }
        }

        public bool IsConnected => _connection != null && _connection.IsOpen && !_disposed;

        public IModel CreateChannel()
        {
            return IsConnected
                ? _connection.CreateModel()
                : throw new InvalidOperationException("No RabbitMQ connections are available to perform this action.");
        }

        public bool TryConnect()
        {
            _logger.LogInformation("RabbitMQ client is trying to connect.");

            lock (SyncRoot)
            {
                var retryPolicy = Policy.Handle<SocketException>()
                    .Or<BrokerUnreachableException>()
                    .WaitAndRetry(
                        _retryCount,
                        retryAttempt => TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)),
                        (exception, timeSpan) =>
                        {
                            _logger.LogWarning(exception.ToString());
                        }
                    );

                retryPolicy.Execute(
                    () =>
                    {
                        _connection = _connectionFactory.CreateConnection();
                    }
                );

                if (IsConnected)
                {
                    _connection.ConnectionShutdown += this.OnConnectionShutdown;

                    _connection.CallbackException += this.OnCallbackException;

                    _connection.ConnectionBlocked += this.OnConnectionBlocked;

                    _logger.LogInformation(
                        $"RabbitMQ persistent connection acquired a connection {_connection.Endpoint.HostName} and is subscribed to failure events."
                    );

                    return true;
                }

                _logger.LogCritical("FATAL ERROR: RabbitMQ connections could not be created and opened.");

                return false;
            }
        }

        private void OnConnectionShutdown(object sender, ShutdownEventArgs eventArgs)
        {
            if (_disposed)
            {
                return;
            }

            _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect...");

            this.TryConnect();
        }

        private void OnCallbackException(object sender, CallbackExceptionEventArgs eventArgs)
        {
            if (_disposed)
            {
                return;
            }

            _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect...");

            this.TryConnect();
        }

        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs eventArgs)
        {
            if (_disposed)
            {
                return;
            }

            _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect...");

            this.TryConnect();
        }
    }
}
