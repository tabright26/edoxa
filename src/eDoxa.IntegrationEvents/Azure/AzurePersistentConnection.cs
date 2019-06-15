// Filename: AzurePersistentConnection.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace eDoxa.IntegrationEvents.Azure
{
    public class AzurePersistentConnection : IAzurePersistentConnection
    {
        private const string TopicName = "edoxa-azure-topic";

        private readonly ILogger<AzurePersistentConnection> _logger;

        private bool _disposed;
        private ITopicClient _client;

        public AzurePersistentConnection(ServiceBusConnectionStringBuilder builder, ILogger<AzurePersistentConnection> logger)
        {
            ConnectionStringBuilder = builder;
            _logger = logger;

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