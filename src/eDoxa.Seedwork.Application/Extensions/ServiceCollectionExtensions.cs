// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac;

using eDoxa.Seedwork.IntegrationEvents;
using eDoxa.Seedwork.IntegrationEvents.AzureServiceBus;
using eDoxa.Seedwork.IntegrationEvents.Infrastructure;
using eDoxa.Seedwork.IntegrationEvents.RabbitMq;
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
                services.AddSingleton<IAzureServiceBusContext>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<AzureServiceBusContext>>();

                        var builder = new ServiceBusConnectionStringBuilder(appSettings.ServiceBus.HostName);

                        return new AzureServiceBusContext(builder, logger);
                    }
                );
            }
            else
            {
                services.AddSingleton<IRabbitMqServiceBusContext>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<RabbitMqServiceBusContext>>();

                        return new RabbitMqServiceBusContext(
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
                services.AddSingleton<IServiceBusPublisher, AzureServiceBusPublisher>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<AzureServiceBusPublisher>>();

                        var scope = provider.GetRequiredService<ILifetimeScope>();

                        var connection = provider.GetRequiredService<IAzureServiceBusContext>();

                        var handler = provider.GetRequiredService<IIntegrationEventSubscriptionStore>();

                        return new AzureServiceBusPublisher(
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
                services.AddSingleton<IServiceBusPublisher, RabbitMqServiceBusPublisher>(
                    provider =>
                    {
                        var logger = provider.GetRequiredService<ILogger<RabbitMqServiceBusPublisher>>();

                        var scope = provider.GetRequiredService<ILifetimeScope>();

                        var connection = provider.GetRequiredService<IRabbitMqServiceBusContext>();

                        var handler = provider.GetRequiredService<IIntegrationEventSubscriptionStore>();

                        var retryCount = appSettings.ServiceBus.RetryCount ?? 5;

                        return new RabbitMqServiceBusPublisher(
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

            services.AddSingleton<IIntegrationEventSubscriptionStore, InMemoryIntegrationEventSubscriptionStore>();
        }
    }
}
