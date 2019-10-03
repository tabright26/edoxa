// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Payment.Api.Infrastructure;
using eDoxa.Seedwork.Monitoring.Extensions;

using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

namespace eDoxa.Payment.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, PaymentAppSettings appSettings)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddCheck("liveness", () => HealthCheckResult.Healthy());
            healthChecks.AddAzureKeyVault(appSettings);
        }
    }
}
