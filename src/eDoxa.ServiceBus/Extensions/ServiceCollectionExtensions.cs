// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using Autofac;

using eDoxa.Security;
using eDoxa.ServiceBus.Azure;
using eDoxa.ServiceBus.RabbitMQ;

using Microsoft.Azure.ServiceBus;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

namespace eDoxa.ServiceBus.Extensions
{
    public static class ServiceCollectionExtensions
    {
        private const string AzureServiceBusEnable = "AzureServiceBus:Enable";
        private const string ServiceBusHostName = "ServiceBus:HostName";
        private const string ServiceBusPort = "ServiceBus:Port";
        private const string ServiceBusUserName = "ServiceBus:UserName";
        private const string ServiceBusPassword = "ServiceBus:Password";
        private const string ServiceBusRetryCount = "ServiceBus:RetryCount";
        private const string ServiceBusSubscription = "ServiceBus:Subscription";

        public static void AddIntegrationEventDbContext(this IServiceCollection services, IConfiguration configuration, Assembly migrationsAssembly)
        {
            services.AddDbContext<IntegrationEventLogDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString(CustomConnectionStrings.SqlServer),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(migrationsAssembly.GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }
                )
            );
        }

        public static void AddServiceBus(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddServiceBusConnection(configuration);

            services.AddEventBus(configuration);
        }

        private static void AddServiceBusConnection(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>(AzureServiceBusEnable))
            {
                services.AddSingleton<IAzurePersistentConnection>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<AzurePersistentConnection>>();

                        var builder = new ServiceBusConnectionStringBuilder(configuration[ServiceBusHostName]);

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
                            HostName = configuration[ServiceBusHostName]
                        };

                        if (!string.IsNullOrEmpty(configuration[ServiceBusPort]))
                        {
                            factory.Port = configuration.GetValue<int>(ServiceBusPort);
                        }

                        if (!string.IsNullOrEmpty(configuration[ServiceBusUserName]))
                        {
                            factory.UserName = configuration[ServiceBusUserName];
                        }

                        if (!string.IsNullOrEmpty(configuration[ServiceBusPassword]))
                        {
                            factory.Password = configuration[ServiceBusPassword];
                        }

                        var retryCount = 5;

                        if (!string.IsNullOrEmpty(configuration[ServiceBusRetryCount]))
                        {
                            retryCount = int.Parse(configuration[ServiceBusRetryCount]);
                        }

                        return new RabbitMqPersistentConnection(factory, logger, retryCount);
                    }
                );
            }
        }

        private static void AddEventBus(this IServiceCollection services, IConfiguration configuration)
        {
            var subscription = configuration[ServiceBusSubscription];

            if (configuration.GetValue<bool>(AzureServiceBusEnable))
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

                        if (!string.IsNullOrEmpty(configuration[ServiceBusRetryCount]))
                        {
                            retryCount = int.Parse(configuration[ServiceBusRetryCount]);
                        }

                        return new RabbitMqEventBusService(logger, scope, connection, handler, retryCount, subscription);
                    }
                );
            }

            services.AddSingleton<ISubscriptionHandler, InMemorySubscriptionHandler>();
        }
    }
}