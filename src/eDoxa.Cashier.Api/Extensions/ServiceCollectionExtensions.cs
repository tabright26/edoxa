// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Cashier.Api.Infrastructure;
using eDoxa.Seedwork.Monitoring.Extensions;

using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Cashier.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, CashierAppSettings appSettings)
        {
            var healthChecks = services.AddHealthChecks();
            healthChecks.AddAzureKeyVault(appSettings);
            healthChecks.AddIdentityServer(appSettings);
            healthChecks.AddSqlServer(appSettings.ConnectionStrings.SqlServer);
            healthChecks.AddUrlGroup(appSettings.HealthChecks.PaymentUrl, "payment-api", new[] { "api" });
        }
    }
}
