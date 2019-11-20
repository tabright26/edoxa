// Filename: HealthChecksBuilderExtensions.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;
using eDoxa.Seedwork.Security;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Monitoring.Extensions
{
    public static class HealthChecksBuilderExtensions
    {
        public static IHealthChecksBuilder AddAzureServiceBusTopic(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            var connectionString = new ServiceBusConnectionStringBuilder(configuration.GetAzureServiceBusConnectionString());

            return builder.AddAzureServiceBusTopic(connectionString.GetNamespaceConnectionString(), connectionString.EntityPath);
        }

        public static IHealthChecksBuilder AddAzureBlobStorage(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddAzureBlobStorage(string.Join(';', configuration.GetAzureBlobStorageConnectionString()!.Split(';').Take(4)));
        }

        public static IHealthChecksBuilder AddRabbitMq(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddRabbitMQ(configuration.GetRabbitMqConnectionString());
        }

        public static IHealthChecksBuilder AddAzureKeyVault(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            var connectionString = new KeyVaultConnectionStringBuilder(configuration.GetAzureKeyVaultConnectionString()!);

            return builder.AddAzureKeyVault(
                options =>
                {
                    options.UseKeyVaultUrl($"https://{connectionString.Name}.vault.azure.net");
                    options.UseClientSecrets(connectionString.ClientId, connectionString.ClientSecret);
                });
        }

        public static IHealthChecksBuilder AddSqlServer(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddSqlServer(configuration.GetSqlServerConnectionString());
        }

        public static IHealthChecksBuilder AddIdentityServer<TEndpointsOptions>(
            this IHealthChecksBuilder builder,
            IHasEndpointsAppSettings<TEndpointsOptions> appSettings
        )
        where TEndpointsOptions : AuthorityEndpointsOptions
        {
            return builder.AddIdentityServer(new Uri(appSettings.Endpoints.IdentityUrl));
        }

        public static IHealthChecksBuilder AddRedis(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddRedis(configuration.GetRedisConnectionString());
        }

        public static IHealthChecksBuilder AddUrlGroup(this IHealthChecksBuilder builder, string? uriString, string name)
        {
            return uriString == null ? builder : builder.AddUrlGroup(new Uri(HealthCheckEndpoint.Parse(uriString)), name);
        }
    }
}
