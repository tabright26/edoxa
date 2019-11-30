// Filename: HealthChecksBuilderExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;
using eDoxa.Seedwork.Monitoring.HealthChecks.Constants;
using eDoxa.Seedwork.Security.AzureKeyVault;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eDoxa.Seedwork.Monitoring.HealthChecks.Extensions
{
    public static class HealthChecksBuilderExtensions
    {
        public static IHealthChecksBuilder AddCustomSelfCheck(this IHealthChecksBuilder builder)
        {
            return builder.AddCheck(HealthCheckEndpoints.LivenessName, () => HealthCheckResult.Healthy());
        }

        public static IHealthChecksBuilder AddAzureServiceBusTopic(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            var connectionString = new ServiceBusConnectionStringBuilder(configuration.GetAzureServiceBusConnectionString());

            return builder.AddAzureServiceBusTopic(connectionString.GetNamespaceConnectionString(), connectionString.EntityPath);
        }

        public static IHealthChecksBuilder AddAzureBlobStorage(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddAzureBlobStorage(string.Join(';', configuration.GetAzureBlobStorageConnectionString()!.Split(';').Take(4)));
        }

        public static IHealthChecksBuilder AddAzureKeyVault(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            var connectionString = new AzureKeyVaultConnectionStringBuilder(configuration.GetAzureKeyVaultConnectionString()!);

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
            return uriString == null ? builder : builder.AddUrlGroup(new Uri(HealthCheckEndpointHelper.Parse(uriString)), name);
        }
    }
}
