// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Organizations.Clans.Api.Infrastructure;
using eDoxa.Seedwork.Monitoring.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eDoxa.Organizations.Clans.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, ClansAppSettings appSettings)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddCheck("liveness", () => HealthCheckResult.Healthy());
            healthChecks.AddAzureKeyVault(appSettings);
            healthChecks.AddIdentityServer(appSettings);
            healthChecks.AddSqlServer(appSettings.ConnectionStrings);
        }
    }
}
