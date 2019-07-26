// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Monitoring.AppSettings;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Web.Gateway.Infrastructure;

using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Web.Gateway.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, WebGatewayAppSettings appSettings)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddUrlGroup(appSettings.HealthChecks.IdentityUrl, "identity-api", new[] {"api"});
            healthChecks.AddUrlGroup(appSettings.HealthChecks.CashierUrl, "cashier-api", new[] {"api"});
            healthChecks.AddUrlGroup(appSettings.HealthChecks.ArenaChallengesUrl, "arena-challenges-api", new[] {"api"});
        }

        public static void AddAuthentication(
            this IServiceCollection services,
            IHostingEnvironment environment,
            IHasAuthorityAppSettings appSettings,
            IDictionary<string, ApiResource> apiResources
        )
        {
            var builder = services.AddAuthentication();

            foreach (var (authenticationScheme, apiResource) in apiResources)
            {
                builder.AddIdentityServerAuthentication(
                    authenticationScheme,
                    options =>
                    {
                        options.ApiName = apiResource.Name;
                        options.Authority = appSettings.Authority.PrivateUrl;
                        options.RequireHttpsMetadata = environment.IsProduction();
                        options.ApiSecret = "secret";
                    }
                );
            }
        }
    }
}
