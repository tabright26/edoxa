// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-08-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDataProtection(this IServiceCollection services, string redisConnectionStrings, string applicationDiscriminator)
        {
            services.AddDataProtection(options => options.ApplicationDiscriminator = applicationDiscriminator)
                .PersistKeysToRedis(ConnectionMultiplexer.Connect(redisConnectionStrings), "data-protection");
        }
    }
}
