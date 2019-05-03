// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Security.Services;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
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
                        options => { options.ApplicationDiscriminator = configuration["ApplicationDiscriminator"]; }
                    )
                    .PersistKeysToRedis(ConnectionMultiplexer.Connect(configuration.GetConnectionString("Redis")), "data-protection");
            }
        }

        public static void AddAuthentication(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment, string audience)
        {
            var authority = configuration.GetValue<string>("IdentityServer:Url");

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.Audience = audience;
                options.Authority = authority;
                options.RequireHttpsMetadata = environment.IsProduction();
            });
        }
    }
}