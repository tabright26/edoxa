// Filename: AzurePersistentConnection.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace eDoxa.ServiceBus.Azure
{
    public class AzurePersistentConnection : IAzurePersistentConnection
    {
        private const string TopicName = "edoxa-azure-topic";

        private readonly ILogger<AzurePersistentConnection> _logger;

        private bool _disposed;
        private ITopicClient _client;

        /// <summary>
        ///     Initializes a new instance of the <see cref="T:eDoxa.EventBus.AzureServiceBus.AzureEventBusPersistentConnection" />
        ///     class.
        /// </summary>
        /// <param name="builder">The <see cref="T:Microsoft.Azure.ServiceBus.ServiceBusConnectionStringBuilder" />.</param>
        /// <param name="logger">The <see cref="T:Microsoft.Extensions.Logging.ILogger" />.</param>
        public AzurePersistentConnection(ServiceBusConnectionStringBuilder builder, ILogger<AzurePersistentConnection> logger)
        {
            ConnectionStringBuilder = builder ?? throw new ArgumentNullException(nameof(builder));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));

            _client = new TopicClient(ConnectionStringBuilder.GetNamespaceConnectionString(), TopicName, RetryPolicy.Default);
        }

        public ServiceBusConnectionStringBuilder ConnectionStringBuilder { get; }

        public ITopicClient CreateTopicClient()
        {
            if (!_client.IsClosedOrClosing)
            {
                return _client;
            }

            _logger.LogInformation("The previous TopicClient has been closed or closing ...");

            _client = new TopicClient(ConnectionStringBuilder.GetNamespaceConnectionString(), TopicName, RetryPolicy.Default);

            _logger.LogInformation("New instance of TopicClient being initialized.");

            return _client;
        }

        public void Dispose()
        {
            if (_disposed)
            {
                return;
            }

            _disposed = true;
        }
    }
}