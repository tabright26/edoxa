// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Games.Api.Infrastructure;
using eDoxa.Seedwork.Monitoring.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eDoxa.Arena.Games.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, GamesAppSettings appSettings)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddCheck("liveness", () => HealthCheckResult.Healthy());
            healthChecks.AddAzureKeyVault(appSettings);
            healthChecks.AddIdentityServer(appSettings);
            healthChecks.AddSqlServer(appSettings.ConnectionStrings);
        }
    }
}
