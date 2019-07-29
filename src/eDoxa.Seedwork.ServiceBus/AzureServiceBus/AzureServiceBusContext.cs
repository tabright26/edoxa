// Filename: AzureServiceBusContext.cs
// Date Created: 2019-07-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Logging;

namespace eDoxa.Seedwork.ServiceBus.AzureServiceBus
{
    public class AzureServiceBusContext : IAzureServiceBusContext
    {
        private const string TopicName = "edoxa-azure-topic";

        private readonly ILogger<AzureServiceBusContext> _logger;

        private bool _disposed;
        private ITopicClient _client;

        public AzureServiceBusContext(ServiceBusConnectionStringBuilder builder, ILogger<AzureServiceBusContext> logger)
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
