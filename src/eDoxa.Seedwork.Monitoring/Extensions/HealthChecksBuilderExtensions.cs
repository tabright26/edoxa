// Filename: HealthChecksBuilderExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Security.Constants;

using JetBrains.Annotations;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Monitoring.Extensions
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
                tags: new[] {"akv", "key-vault"}
            );
        }

        public static void AddSqlServer(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            builder.AddSqlServer(
                configuration.GetConnectionString(CustomConnectionStrings.SqlServer),
                name: "microsoft-sql-server",
                tags: new[] {"mssql", "sql", "sql-server"}
            );
        }

        public static void AddIdentityServer(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            builder.AddIdentityServer(new Uri(configuration["AppSettings:Authority:PrivateUrl"]), "identity-server", tags: new[] {"idsrv"});
        }

        public static void AddRedis(this IHealthChecksBuilder builder, IConfiguration configuration)
        {
            builder.AddRedis(configuration.GetConnectionString(CustomConnectionStrings.Redis), "redis", tags: new[] {"cache"});
        }

        public static void AddUrlGroup(
            this IHealthChecksBuilder builder,
            [CanBeNull] string uriString,
            string name,
            IEnumerable<string> tags
        )
        {
            if (uriString != null)
            {
                builder.AddUrlGroup(new Uri(uriString), name, tags: tags);
            }
        }
    }
}
