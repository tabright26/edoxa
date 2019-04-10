// Filename: RabbitMqPersistentConnection.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.IO;
using System.Net.Sockets;

using eDoxa.ServiceBus.Exceptions;

using Microsoft.Extensions.Logging;

using Polly;

using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;

namespace eDoxa.ServiceBus.RabbitMQ
{
    public class RabbitMqPersistentConnection : IRabbitMqPersistentConnection
    {
        private static readonly object SyncRoot = new object();

        protected IConnection _connection;

        private readonly int _retryCount;
        private readonly IConnectionFactory _connectionFactory;
        private readonly ILogger<RabbitMqPersistentConnection> _logger;

        private bool _disposed;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:eDoxa.EventBus.RabbitMQ.RabbitMqEventBusPersistentConnection" />
        ///     class.
        /// </summary>
        /// <param name="connectionFactory">The <see cref="T:RabbitMQ.Client.IConnectionFactory" />.</param>
        /// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" />.</param>
        /// <param name="retryCount">The retry count of connection attempt.</param>
        public RabbitMqPersistentConnection(IConnectionFactory connectionFactory, ILogger<RabbitMqPersistentConnection> logger, int retryCount = 5)
        {
            _connectionFactory = connectionFactory ??
                                 throw new RabbitMqException(nameof(RabbitMqPersistentConnection), new ArgumentNullException(nameof(connectionFactory)));

            _logger = logger ?? throw new RabbitMqException(nameof(RabbitMqPersistentConnection), new ArgumentNullException(nameof(logger)));

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

        public bool IsConnected
        {
            get
            {
                return _connection != null && _connection.IsOpen && !_disposed;
            }
        }

        public IModel CreateChannel()
        {
            if (!IsConnected)
            {
                throw new RabbitMqException(
                    nameof(this.CreateChannel),
                    new InvalidOperationException("No RabbitMQ connections are available to perform this action.")
                );
            }

            return _connection.CreateModel();
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

        /// <summary>Raised when the connection is destroyed.</summary>
        /// <remarks>
        ///     If the connection is already destroyed at the time an
        ///     event handler is added to this event, the event handler
        ///     will be fired immediately.
        /// </remarks>
        /// <param name="sender">The object that invoked the event that fired the event handler.</param>
        /// <param name="eventArgs">The <see cref="T:RabbitMQ.Client.ShutdownEventArgs" />.</param>
        private void OnConnectionShutdown(object sender, ShutdownEventArgs eventArgs)
        {
            if (_disposed)
            {
                return;
            }

            _logger.LogWarning("A RabbitMQ connection is on shutdown. Trying to re-connect ...");

            this.TryConnect();
        }

        /// <summary>
        ///     Signalled when an exception occurs in a callback invoked by the connection.
        /// </summary>
        /// <remarks>
        ///     This event is signalled when a ConnectionShutdown handler
        ///     throws an exception. If, in future, more events appear on
        ///     <see cref="T:RabbitMQ.Client.IConnection" />, then this event will be signalled whenever one
        ///     of those event handlers throws an exception, as well.
        /// </remarks>
        /// <param name="sender">The object that invoked the event that fired the event handler.</param>
        /// <param name="eventArgs">The <see cref="T:RabbitMQ.Client.CallbackExceptionEventArgs" />.</param>
        private void OnCallbackException(object sender, CallbackExceptionEventArgs eventArgs)
        {
            if (_disposed)
            {
                return;
            }

            _logger.LogWarning("A RabbitMQ connection throw exception. Trying to re-connect ...");

            this.TryConnect();
        }

        /// <summary>
        ///     Raised when the connection is blocked.
        /// </summary>
        /// <param name="sender">The object that invoked the event that fired the event handler.</param>
        /// <param name="eventArgs">The <see cref="T:RabbitMQ.Client.ConnectionBlockedEventArgs" />.</param>
        private void OnConnectionBlocked(object sender, ConnectionBlockedEventArgs eventArgs)
        {
            if (_disposed)
            {
                return;
            }

            _logger.LogWarning("A RabbitMQ connection is shutdown. Trying to re-connect ...");

            this.TryConnect();
        }
    }
}