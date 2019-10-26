// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis.Extensions.Newtonsoft;

using static StackExchange.Redis.ConnectionMultiplexer;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddRedis(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddStackExchangeRedisExtensions<NewtonsoftSerializer>(Connect(configuration.GetConnectionString(ConnectionStrings.Redis)));
        }
    }
}
