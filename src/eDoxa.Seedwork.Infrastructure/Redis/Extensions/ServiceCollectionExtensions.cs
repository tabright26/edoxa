// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-12-18
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis.Extensions.Core;
using StackExchange.Redis.Extensions.Core.Abstractions;
using StackExchange.Redis.Extensions.Core.Configuration;
using StackExchange.Redis.Extensions.Core.Implementations;
using StackExchange.Redis.Extensions.Newtonsoft;

namespace eDoxa.Seedwork.Infrastructure.Redis.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomRedis(this IServiceCollection services, IConfiguration configuration)
        {
            var connectionString = configuration.GetRedisConnectionString();

            var split = connectionString.Split(':', StringSplitOptions.RemoveEmptyEntries);

            if (split.Length == 2)
            {
                services.AddSingleton(
                    new RedisConfiguration
                    {
                        Hosts = new[]
                        {
                            new RedisHost
                            {
                                Host = split[0],
                                Port = Convert.ToInt32(split[1])
                            }
                        }
                    });
            }
            else
            {
                services.AddSingleton(
                    new RedisConfiguration
                    {
                        Hosts = new[]
                        {
                            new RedisHost
                            {
                                Host = split[0]
                            }
                        }
                    });
            }

            services.AddSingleton<IRedisCacheClient, RedisCacheClient>();
            services.AddSingleton<IRedisCacheConnectionPoolManager, RedisCacheConnectionPoolManager>();
            services.AddSingleton<IRedisDefaultCacheClient, RedisDefaultCacheClient>();
            services.AddSingleton<ISerializer, NewtonsoftSerializer>();
        }
    }
}
