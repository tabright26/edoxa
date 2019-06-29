// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Monitoring;
using eDoxa.Monitoring.Extensions;
using eDoxa.Web.Aggregator.DelegatingHandlers;
using eDoxa.Web.Aggregator.Services;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Web.Aggregator.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecks = services.AddHealthChecks();

            healthChecks.AddIdentityServer(configuration);

            healthChecks.AddUrlGroup(new Uri(configuration["HealthChecks:Identity:Url"]), "identity-api", tags: new[] {"api"});

            healthChecks.AddUrlGroup(new Uri(configuration["HealthChecks:Cashier:Url"]), "cashier-api", tags: new[] {"api"});

            healthChecks.AddUrlGroup(new Uri(configuration["HealthChecks:ArenaChallenges:Url"]), "arena-challenges-api", tags: new[] {"api"});
        }

        public static void AddServices(this IServiceCollection services)
        {
            services.AddTransient<HttpAuthorizationDelegatingHandler>();

            services.AddHttpClient<IIdentityService, IdentityService>()
                .AddHttpMessageHandler<HttpAuthorizationDelegatingHandler>()
                .AddPolicyHandler(HttpPolicies.GetRetryPolicy())
                .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy());

            services.AddHttpClient<IArenaChallengesService, ArenaChallengesService>()
                .AddHttpMessageHandler<HttpAuthorizationDelegatingHandler>()
                .AddPolicyHandler(HttpPolicies.GetRetryPolicy())
                .AddPolicyHandler(HttpPolicies.GetCircuitBreakerPolicy());
        }
    }
}
