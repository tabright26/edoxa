// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-03-04
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using Autofac;

using eDoxa.ServiceBus.Azure;
using eDoxa.ServiceBus.RabbitMQ;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

namespace eDoxa.ServiceBus.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string UseAzureServiceBusFromConfig = "UseAzureServiceBus";
        private const string ServiceBusHostNameFromConfig = "ServiceBus:HostName";
        private const string ServiceBusUserNameFromConfig = "ServiceBus:UserName";
        private const string ServiceBusPasswordFromConfig = "ServiceBus:Password";
        private const string ServiceBusRetryCountFromConfig = "ServiceBus:RetryCount";
        private const string ServiceBusSubscriptionFromConfig = "ServiceBus:Subscription";

        public static void AddServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>(UseAzureServiceBusFromConfig))
            {
                services.AddSingleton<IAzurePersistentConnection>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<AzurePersistentConnection>>();

                        var builder = new ServiceBusConnectionStringBuilder(configuration[ServiceBusHostNameFromConfig]);

                        return new AzurePersistentConnection(builder, logger);
                    }
                );
            }
            else
            {
                services.AddSingleton<IRabbitMqPersistentConnection>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<RabbitMqPersistentConnection>>();

                        var factory = new ConnectionFactory
                        {
                            HostName = configuration[ServiceBusHostNameFromConfig]
                        };

                        if (!string.IsNullOrEmpty(configuration[ServiceBusUserNameFromConfig]))
                        {
                            factory.UserName = configuration[ServiceBusUserNameFromConfig];
                        }

                        if (!string.IsNullOrEmpty(configuration[ServiceBusPasswordFromConfig]))
                        {
                            factory.Password = configuration[ServiceBusPasswordFromConfig];
                        }

                        var retryCount = 5;

                        if (!string.IsNullOrEmpty(configuration[ServiceBusRetryCountFromConfig]))
                        {
                            retryCount = int.Parse(configuration[ServiceBusRetryCountFromConfig]);
                        }

                        return new RabbitMqPersistentConnection(factory, logger, retryCount);
                    }
                );
            }
        }

        public static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscription = configuration[ServiceBusSubscriptionFromConfig];

            if (configuration.GetValue<bool>(UseAzureServiceBusFromConfig))
            {
                services.AddSingleton<IEventBusService, AzureEventBusService>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<AzureEventBusService>>();

                        var scope = provider.GetRequiredService<ILifetimeScope>();

                        var connection = provider.GetRequiredService<IAzurePersistentConnection>();

                        var handler = provider.GetRequiredService<ISubscriptionHandler>();

                        return new AzureEventBusService(logger, scope, connection, handler, subscription);
                    }
                );
            }
            else
            {
                services.AddSingleton<IEventBusService, RabbitMqEventBusService>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<RabbitMqEventBusService>>();

                        var scope = provider.GetRequiredService<ILifetimeScope>();

                        var connection = provider.GetRequiredService<IRabbitMqPersistentConnection>();

                        var handler = provider.GetRequiredService<ISubscriptionHandler>();

                        var retryCount = 5;

                        if (!string.IsNullOrEmpty(configuration[ServiceBusRetryCountFromConfig]))
                        {
                            retryCount = int.Parse(configuration[ServiceBusRetryCountFromConfig]);
                        }

                        return new RabbitMqEventBusService(logger, scope, connection, handler, retryCount, subscription);
                    }
                );
            }

            services.AddSingleton<ISubscriptionHandler, InMemorySubscriptionHandler>();
        }
    }
}