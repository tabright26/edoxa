// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Monitoring.Extensions;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Web.Gateway.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddUrlGroup(configuration["HealthChecks:Identity:Url"], "identity-api", new[] {"api"});
            healthChecks.AddUrlGroup(configuration["HealthChecks:Cashier:Url"], "cashier-api", new[] {"api"});
            healthChecks.AddUrlGroup(configuration["HealthChecks:ArenaChallenges:Url"], "arena-challenges-api", new[] {"api"});
        }
    }
}
