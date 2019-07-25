// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Identity.Api.Infrastructure;
using eDoxa.Seedwork.Monitoring.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, IdentityAppSettings appSettings)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddAzureKeyVault(appSettings);
            healthChecks.AddSqlServer(appSettings.ConnectionStrings);
            healthChecks.AddRedis(appSettings.ConnectionStrings);
        }
    }
}
