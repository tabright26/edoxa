// Filename: HealthChecksBuilderExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Linq;

using eDoxa.Seedwork.Application.AppSettings;
using eDoxa.Seedwork.Application.AppSettings.Options;
using eDoxa.Seedwork.Application.HealthChecks.Constants;
using eDoxa.Seedwork.Infrastructure.AzureKeyVault;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.Azure.ServiceBus;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eDoxa.Seedwork.Application.HealthChecks.Extensions
{
    public static class HealthChecksBuilderExtensions
    {
        public static IHealthChecksBuilder AddCustomSelfCheck(this IHealthChecksBuilder builder)
        {
            return builder.AddCheck(HealthCheckEndpoints.LivenessName, () => HealthCheckResult.Healthy());
        }

        public static IHealthChecksBuilder AddCustomAzureServiceBusTopic(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            var connectionString = new ServiceBusConnectionStringBuilder(configuration.GetAzureServiceBusConnectionString());

            return builder.AddAzureServiceBusTopic(connectionString.GetNamespaceConnectionString(), connectionString.EntityPath);
        }

        public static IHealthChecksBuilder AddCustomAzureBlobStorage(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddAzureBlobStorage(string.Join(';', configuration.GetAzureBlobStorageConnectionString().Split(';').Take(4)));
        }

        public static IHealthChecksBuilder AddCustomAzureKeyVault(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            var connectionString = new AzureKeyVaultConnectionStringBuilder(configuration.GetAzureKeyVaultConnectionString());

            return builder.AddAzureKeyVault(
                options =>
                {
                    options.UseKeyVaultUrl($"https://{connectionString.Name}.vault.azure.net");
                    options.UseClientSecrets(connectionString.ClientId, connectionString.ClientSecret);
                });
        }

        public static IHealthChecksBuilder AddCustomSqlServer(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddSqlServer(configuration.GetSqlServerConnectionString());
        }

        public static IHealthChecksBuilder AddCustomIdentityServer<TEndpointsOptions>(
            this IHealthChecksBuilder builder,
            IHasEndpointsAppSettings<TEndpointsOptions> appSettings
        )
        where TEndpointsOptions : AuthorityEndpointsOptions
        {
            return builder.AddIdentityServer(new Uri(appSettings.Endpoints.IdentityUrl));
        }

        public static IHealthChecksBuilder AddCustomRedis(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            return builder.AddRedis(configuration.GetRedisConnectionString());
        }

        public static IHealthChecksBuilder AddCustomUrlGroup(this IHealthChecksBuilder builder, string uriString, string name)
        {
            return builder.AddUrlGroup(new Uri(HealthCheckEndpointHelper.Parse(uriString)), name);
        }
    }
}
