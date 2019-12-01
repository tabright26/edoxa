// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Security.AzureKeyVault;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static StackExchange.Redis.ConnectionMultiplexer;

namespace eDoxa.Seedwork.Security.DataProtection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomDataProtection(this IServiceCollection services, IConfiguration configuration, string discriminator)
        {
            var connectionString = new AzureKeyVaultConnectionStringBuilder(configuration.GetConnectionString("AzureKeyVault"));

            services.AddDataProtection(options => options.ApplicationDiscriminator = discriminator)
                .PersistKeysToRedis(Connect(configuration.GetConnectionString("Redis")), "DataProtection-Keys")
                .ProtectKeysWithAzureKeyVault(
                    $"https://{connectionString.Name}.vault.azure.net/keys/dataprotection/",
                    connectionString.ClientId,
                    connectionString.ClientSecret);
        }
    }
}
