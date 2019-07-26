// Filename: AzurePersistentConnectionTest.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.IntegrationEvents.Azure;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.VisualStudio.TestTools.UnitTesting;

using Moq;

namespace eDoxa.Seedwork.UnitTests.IntegrationEvents.Azure
{
    [TestClass]
    public sealed class AzurePersistentConnectionTest
    {
        private const string ConnectionString =
            "Endpoint=sb://edoxa-development.servicebus.windows.net/;SharedAccessKeyName=RootManageSharedAccessKey;SharedAccessKey=vk+lF3pvAOv04F6/rlK6TtFrNui9BIjimW3Rn/ZW4VE=";

        [TestMethod]
        public void CreateTopicClient_FromAzurePersistentConnection_ShouldBeNotNull()
        {
            // Arrange
            var persitentConnection = GetAzurePersistentConnection();

            // Act
            var topicClient = persitentConnection.CreateTopicClient();

            // Assert
            Assert.IsNotNull(topicClient);
        }

        private static IAzurePersistentConnection GetAzurePersistentConnection()
        {
            var services = new ServiceCollection();

            services.AddSingleton<IAzurePersistentConnection>(
                serviceProvider =>
                {
                    var logger = new Mock<ILogger<AzurePersistentConnection>>();

                    var builder = new ServiceBusConnectionStringBuilder(ConnectionString);

                    return new AzurePersistentConnection(builder, logger.Object);
                }
            );

            var provider = services.BuildServiceProvider();

            return provider.GetService<IAzurePersistentConnection>();
        }
    }
}
