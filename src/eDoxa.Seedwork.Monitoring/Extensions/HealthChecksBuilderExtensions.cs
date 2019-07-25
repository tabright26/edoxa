// Filename: HealthChecksBuilderExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;

using eDoxa.Seedwork.Monitoring.AppSettings;

using JetBrains.Annotations;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Monitoring.Extensions
{
    public static class HealthChecksBuilderExtensions
    {
        public static void AddAzureKeyVault(this IHealthChecksBuilder builder, IHasAzureKeyVaultAppSettings appSettings)
        {
            builder.AddAzureKeyVault(
                options =>
                {
                    options.UseKeyVaultUrl($"https://{appSettings.AzureKeyVault.Name}.vault.azure.net");
                    options.UseClientSecrets(appSettings.AzureKeyVault.ClientId, appSettings.AzureKeyVault.ClientSecret);
                },
                "azure-key-vault",
                tags: new[] {"akv", "key-vault"}
            );
        }

        public static void AddSqlServer(this IHealthChecksBuilder builder, string connectionString)
        {
            builder.AddSqlServer(connectionString, name: "microsoft-sql-server", tags: new[] {"mssql", "sql", "sql-server"});
        }

        public static void AddIdentityServer(this IHealthChecksBuilder builder, IHasAuthorityAppSettings appSettings)
        {
            builder.AddIdentityServer(new Uri(appSettings.Authority.PrivateUrl), "identity-server", tags: new[] {"idsrv"});
        }

        public static void AddRedis(this IHealthChecksBuilder builder, string connectionString)
        {
            builder.AddRedis(connectionString, "redis", tags: new[] {"cache"});
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
