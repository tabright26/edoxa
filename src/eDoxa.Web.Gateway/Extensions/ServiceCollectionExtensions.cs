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
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eDoxa.Web.Gateway.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, WebGatewayAppSettings appSettings)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddCheck("liveness", () => HealthCheckResult.Healthy());
            healthChecks.AddUrlGroup(appSettings.HealthChecks.IdentityUrl, "identity-api", new[] {"api", "identity"});
            healthChecks.AddUrlGroup(appSettings.HealthChecks.CashierUrl, "cashier-api", new[] {"api", "cashier" });
            healthChecks.AddUrlGroup(appSettings.HealthChecks.PaymentUrl, "payment-api", new[] {"api", "payment"});
            healthChecks.AddUrlGroup(appSettings.HealthChecks.ArenaChallengesUrl, "arena-challenges-api", new[] {"api", "arena", "challenges" });
            healthChecks.AddUrlGroup(appSettings.HealthChecks.ArenaGamesUrl, "arena-games-api", new[] {"api", "arena", "games" });
            healthChecks.AddUrlGroup(appSettings.HealthChecks.ArenaGamesLeagueOfLegendsUrl, "arena-games-leagueoflegends-api", new[] {"api", "arena", "games", "leagueoflegends" });
            healthChecks.AddUrlGroup(appSettings.HealthChecks.OrganizationsClansUrl, "organizations-clans-api", new[] {"api", "organizations", "clans"});
        }

        public static void AddAuthentication(
            this IServiceCollection services,
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
                        options.RequireHttpsMetadata = false;
                        options.ApiSecret = "secret";
                    }
                );
            }
        }
    }
}
