// Filename: ConfigurationExtensions.cs
// Date Created: 2019-11-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Infrastructure.Constants;

using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class ConfigurationExtensions
    {
        public static string? GetAzureKeyVaultConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(ConnectionStrings.AzureKeyVault);
        }

        public static string? GetAzureServiceBusConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(ConnectionStrings.AzureServiceBus);
        }

        public static string? GetAzureBlobStorageConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(ConnectionStrings.AzureBlobStorage);
        }

        public static string? GetRabbitMqConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(ConnectionStrings.RabbitMq);
        }

        public static string? GetRedisConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(ConnectionStrings.Redis);
        }

        public static string? GetSqlServerConnectionString(this IConfiguration configuration)
        {
            return configuration.GetConnectionString(ConnectionStrings.SqlServer);
        }
    }
}
