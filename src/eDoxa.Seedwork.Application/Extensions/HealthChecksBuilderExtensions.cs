// Filename: HealthChecksBuilderExtensions.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Security;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class HealthChecksBuilderExtensions
    {
        private const string AzureKeyVaultName = "AzureKeyVault:Name";
        private const string AzureKeyVaultClientId = "AzureKeyVault:ClientId";
        private const string AzureKeyVaultClientSecret = "AzureKeyVault:ClientSecret";

        public static void AddAzureKeyVault(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            builder.AddAzureKeyVault(
                options =>
                {
                    options.UseKeyVaultUrl($"https://{configuration[AzureKeyVaultName]}.vault.azure.net");
                    options.UseClientSecrets(configuration[AzureKeyVaultClientId], configuration[AzureKeyVaultClientSecret]);
                },
                "azure-key-vault",
                tags: new[]
                {
                    "akv", "key-vault"
                }
            );
        }

        public static void AddSqlServer(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            builder.AddSqlServer(
                configuration.GetConnectionString(CustomConnectionStrings.SqlServer),
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
                new Uri(configuration["IdentityServer:Url"]),
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
                configuration.GetConnectionString(CustomConnectionStrings.Redis),
                "redis",
                tags: new[]
                {
                    "cache"
                }
            );
        }
    }
}