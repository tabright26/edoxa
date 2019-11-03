// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using static StackExchange.Redis.ConnectionMultiplexer;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataProtection(this IServiceCollection services, IConfiguration configuration, string discriminator)
        {
            if (configuration.GetValue<bool>("AzureKubernetesServiceEnabled"))
            {
                var connectionString = new KeyVaultConnectionStringBuilder(configuration.GetConnectionString("AzureKeyVault"));

                services.AddDataProtection(options => options.ApplicationDiscriminator = discriminator)
                    .PersistKeysToRedis(Connect(configuration.GetConnectionString("Redis")), "DataProtection-Keys")
                    .ProtectKeysWithAzureKeyVault(
                        $"https://{connectionString.Name}.vault.azure.net/keys/dataprotection/",
                        connectionString.ClientId,
                        connectionString.ClientSecret);
            }
        }
    }
}
