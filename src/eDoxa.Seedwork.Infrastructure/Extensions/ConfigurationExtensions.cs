// Filename: ConfigurationExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Infrastructure.Constants;

using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string GetAzureKeyVaultConnectionString(this IConfiguration configuration)
        {
            return configuration.TryGetConnectionString(ConnectionStrings.AzureKeyVault);
        }

        public static string GetAzureServiceBusConnectionString(this IConfiguration configuration)
        {
            return configuration.TryGetConnectionString(ConnectionStrings.AzureServiceBus);
        }

        public static string GetAzureBlobStorageConnectionString(this IConfiguration configuration)
        {
            return configuration.TryGetConnectionString(ConnectionStrings.AzureBlobStorage);
        }

        public static string GetRabbitMqConnectionString(this IConfiguration configuration)
        {
            return configuration.TryGetConnectionString(ConnectionStrings.RabbitMq);
        }

        public static string GetRedisConnectionString(this IConfiguration configuration)
        {
            return configuration.TryGetConnectionString(ConnectionStrings.Redis);
        }

        public static string GetSqlServerConnectionString(this IConfiguration configuration)
        {
            return configuration.TryGetConnectionString(ConnectionStrings.SqlServer);
        }

        private static string TryGetConnectionString(this IConfiguration configuration, string name)
        {
            var connectionString = configuration.GetConnectionString(name);

            if (connectionString == null)
            {
                throw new InvalidOperationException($"The {name} connection string wasn't found in ConnectionStrings option from the appsettings.json.");
            }

            return connectionString;
        }
    }
}
