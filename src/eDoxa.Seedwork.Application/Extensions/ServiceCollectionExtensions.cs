// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac;

using eDoxa.IntegrationEvents;
using eDoxa.IntegrationEvents.Azure;
using eDoxa.IntegrationEvents.RabbitMQ;
using eDoxa.Seedwork.Monitoring.AppSettings;

using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

using RabbitMQ.Client;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthentication(this IServiceCollection services, IHostingEnvironment environment, IHasApiResourceAppSettings appSettings)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    GetIdentityServerAuthenticationOptions(environment, appSettings.ApiResource, appSettings.Authority.PrivateUrl)
                );
        }

        public static Action<IdentityServerAuthenticationOptions> GetIdentityServerAuthenticationOptions(
            IHostingEnvironment environment,
            ApiResource apiResource,
            string authority
        )
        {
            return options =>
            {
                options.ApiName = apiResource.Name;
                options.Authority = authority;
                options.RequireHttpsMetadata = environment.IsProduction();
                options.ApiSecret = "secret";
            };
        }

        public static void AddServiceBus(this IServiceCollection services, IHasServiceBusAppSettings appSettings)
        {
            services.AddServiceBusConnection(appSettings);

            services.AddEventBus(appSettings);
        }

        private static void AddServiceBusConnection(this IServiceCollection services, IHasServiceBusAppSettings appSettings)
        {
            if (appSettings.AzureServiceBusEnabled)
            {
                services.AddSingleton<IAzurePersistentConnection>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<AzurePersistentConnection>>();

                        var builder = new ServiceBusConnectionStringBuilder(appSettings.ServiceBus.HostName);

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

                        return new RabbitMqPersistentConnection(
                            new ConnectionFactory
                            {
                                HostName = appSettings.ServiceBus.HostName,
                                Port = appSettings.ServiceBus.Port ?? -1,
                                UserName = appSettings.ServiceBus.UserName ?? "guest",
                                Password = appSettings.ServiceBus.Password ?? "guest"
                            },
                            logger,
                            appSettings.ServiceBus.RetryCount ?? 5
                        );
                    }
                );
            }
        }

        private static void AddEventBus(this IServiceCollection services, IHasServiceBusAppSettings appSettings)
        {
            if (appSettings.AzureServiceBusEnabled)
            {
                services.AddSingleton<IEventBusService, AzureEventBusService>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<AzureEventBusService>>();

                        var scope = provider.GetRequiredService<ILifetimeScope>();

                        var connection = provider.GetRequiredService<IAzurePersistentConnection>();

                        var handler = provider.GetRequiredService<ISubscriptionHandler>();

                        return new AzureEventBusService(
                            logger,
                            scope,
                            connection,
                            handler,
                            appSettings.ServiceBus.SubscriptionName
                        );
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

                        var retryCount = appSettings.ServiceBus.RetryCount ?? 5;

                        return new RabbitMqEventBusService(
                            logger,
                            scope,
                            connection,
                            handler,
                            retryCount,
                            appSettings.ServiceBus.SubscriptionName
                        );
                    }
                );
            }

            services.AddSingleton<ISubscriptionHandler, InMemorySubscriptionHandler>();
        }
    }
}
