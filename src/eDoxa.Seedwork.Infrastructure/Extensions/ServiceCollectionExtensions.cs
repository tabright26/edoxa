// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRedisCache(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddSingleton(
                new RedisConfiguration
                {
                    Hosts = new[]
                    {
                        new RedisHost
                        {
                            Host = configuration.GetRedisConnectionString()
                        }
                    }
                });

            services.AddSingleton<IRedisCacheClient, RedisCacheClient>();
            services.AddSingleton<IRedisCacheConnectionPoolManager, RedisCacheConnectionPoolManager>();
            services.AddSingleton<IRedisDefaultCacheClient, RedisDefaultCacheClient>();
            services.AddSingleton<ISerializer, NewtonsoftSerializer>();
        }
    }
}
