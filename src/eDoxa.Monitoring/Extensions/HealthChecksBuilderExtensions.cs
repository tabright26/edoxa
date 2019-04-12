// Filename: HealthChecksBuilderExtensions.cs
// Date Created: 2019-04-12
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Monitoring.Extensions
{
    public static class HealthChecksBuilderExtensions
    {
        private const string KeyVaultConfigurationEnabled = "ASPNETCORE_HOSTINGSTARTUP:KEYVAULT:CONFIGURATIONENABLED";
        private const string KeyVaultConfigurationVault = "ASPNETCORE_HOSTINGSTARTUP:KEYVAULT:CONFIGURATIONVAULT";
        private const string KeyVaultClientId = "KeyVault:ClientId";
        private const string KeyVaultClientSecret = "KeyVault:ClientSecret";

        public static void AddAzureKeyVault(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>(KeyVaultConfigurationEnabled))
            {
                builder.AddAzureKeyVault(
                    options =>
                    {
                        options.UseKeyVaultUrl(configuration[KeyVaultConfigurationVault]);
                        options.UseClientSecrets(configuration[KeyVaultClientId], configuration[KeyVaultClientSecret]);
                    },
                    "azure-key-vault",
                    tags: new[]
                    {
                        "akv", "key-vault"
                    }
                );
            }
        }

        public static void AddSqlServer(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            builder.AddSqlServer(
                configuration.GetConnectionString("SqlServer"),
                name: "microsoft-sql-server",
                tags: new[]
                {
                    "mssql", "sql", "sql-server"
                }
            );
        }

        public static void AddIdentityServer(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            builder.AddIdentityServer(
                new Uri(configuration["Authority:Internal"]),
                "identity-server",
                tags: new[]
                {
                    "idsrv"
                }
            );
        }

        public static void AddRedis(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            builder.AddRedis(
                configuration.GetConnectionString("Redis"),
                "redis",
                tags: new[]
                {
                    "cache"
                }
            );
        }
    }
}