// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Seedwork.Monitoring.AppSettings;

using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthentication(this IServiceCollection services, IHostingEnvironment environment, IHasApiResourceAppSettings appSettings)
        {
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(
                    GetIdentityServerAuthenticationOptions(environment, appSettings.ApiResource, appSettings.Authority.PrivateUrl)
                );
        }

        public static Action<IdentityServerAuthenticationOptions> GetIdentityServerAuthenticationOptions(
            IHostingEnvironment environment,
            ApiResource apiResource,
            string authority
        )
        {
            return options =>
            {
                options.ApiName = apiResource.Name;
                options.Authority = authority;
                options.RequireHttpsMetadata = environment.IsProduction();
                options.ApiSecret = "secret";
            };
        }
    }
}
