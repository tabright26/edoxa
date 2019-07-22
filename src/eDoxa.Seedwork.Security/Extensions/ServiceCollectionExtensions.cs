// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Security.Constants;

using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

namespace eDoxa.Seedwork.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(
                        CustomPolicies.CorsPolicy,
                        builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(_ => true)
                    );
                }
            );
        }

        // TODO: THIS IS NOT TESTED.
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
                    .PersistKeysToRedis(ConnectionMultiplexer.Connect(configuration.GetConnectionString(CustomConnectionStrings.Redis)), "data-protection");
            }
        }

        public static void AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment environment,
            IDictionary<string, ApiResource> apiResources
        )
        {
            var builder = services.AddAuthentication();

            foreach (var apiResource in apiResources)
            {
                builder.AddIdentityServerAuthentication(
                    apiResource.Key,
                    options =>
                    {
                        options.Authority = configuration.GetValue<string>("AppSettings:Authority:PrivateUrl");
                        options.ApiName = apiResource.Value.Name;
                        options.ApiSecret = "secret";
                        options.RequireHttpsMetadata = environment.IsProduction();
                    }
                );
            }
        }

        public static void AddAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment environment,
            ApiResource apiResource
        )
        {
            var authority = configuration.GetValue<string>("AppSettings:Authority:PrivateUrl");

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    options =>
                    {
                        options.ApiName = apiResource.Name;
                        options.Authority = authority;
                        options.RequireHttpsMetadata = environment.IsProduction();
                        options.ApiSecret = "secret";
                    }
                );
        }
    }
}
