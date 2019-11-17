// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.AppSettings.Options;

using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Gateway.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddAuthentication<TEndpointsOptions>(
            this IServiceCollection services,
            IHasEndpointsAppSettings<TEndpointsOptions> appSettings,
            IDictionary<string, ApiResource> apiResources
        )
        where TEndpointsOptions : AuthorityEndpointsOptions
        {
            var builder = services.AddAuthentication();

            foreach (var (authenticationScheme, apiResource) in apiResources)
            {
                builder.AddIdentityServerAuthentication(
                    authenticationScheme,
                    options =>
                    {
                        options.ApiName = apiResource.Name;
                        options.Authority = appSettings.Endpoints.IdentityUrl;
                        options.RequireHttpsMetadata = false;
                        options.ApiSecret = "secret";
                    });
            }
        }
    }
}
