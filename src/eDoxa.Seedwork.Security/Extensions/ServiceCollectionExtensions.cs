// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-25
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
            services.AddDataProtection(options => options.ApplicationDiscriminator = discriminator)
                .PersistKeysToRedis(Connect(configuration.GetConnectionString("Redis")), "DataProtection-Keys")
                .ProtectKeysWithAzureKeyVault(
                    $"https://{configuration["AzureKeyVault:Name"]}.vault.azure.net/keys/dataprotection/",
                    configuration["AzureKeyVault:ClientId"],
                    configuration["AzureKeyVault:ClientSecret"]);
        }
    }
}
