// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-09-16
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Infrastructure;
using eDoxa.Seedwork.Monitoring.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eDoxa.Arena.Challenges.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, ArenaChallengesAppSettings appSettings)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddCheck("liveness", () => HealthCheckResult.Healthy());
            healthChecks.AddAzureKeyVault(appSettings);
            healthChecks.AddIdentityServer(appSettings);
            healthChecks.AddSqlServer(appSettings.ConnectionStrings);
            healthChecks.AddRedis(appSettings.ConnectionStrings);
        }
    }
}
