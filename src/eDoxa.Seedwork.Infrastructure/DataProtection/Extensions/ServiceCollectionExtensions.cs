// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Redis;
using eDoxa.Seedwork.Infrastructure.AzureKeyVault;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Infrastructure.DataProtection.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomDataProtection(this IServiceCollection services, IConfiguration configuration, string discriminator)
        {
            var connectionString = new AzureKeyVaultConnectionStringBuilder(configuration.GetConnectionString("AzureKeyVault"));

            services.AddDataProtection(options => options.ApplicationDiscriminator = discriminator)
                .PersistKeysToRedis(RedisConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")), "DataProtection-Keys")
                .ProtectKeysWithAzureKeyVault(
                    $"https://{connectionString.Name}.vault.azure.net/keys/dataprotection/",
                    connectionString.ClientId,
                    connectionString.ClientSecret);
        }
    }
}
