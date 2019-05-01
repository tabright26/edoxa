// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Security.Services;

using Microsoft.AspNetCore.DataProtection;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

namespace eDoxa.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddUserInfo(this IServiceCollection services)
        {
            services.AddSingleton<IUserInfoService, UserInfoService>();
        }

        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(CustomPolicies.CorsPolicy, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                }
            );
        }

        public static void AddDataProtection(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("AzureKubernetesService:Enable"))
            {
                services.AddDataProtection(
                            options =>
                            {
                                options.ApplicationDiscriminator = configuration["ApplicationDiscriminator"];
                            }
                        )
                        .PersistKeysToRedis(ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")), "data-protection");
            }
        }
    }
}