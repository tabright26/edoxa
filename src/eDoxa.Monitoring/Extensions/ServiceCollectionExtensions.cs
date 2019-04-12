// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-12
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using HealthChecks.UI.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Monitoring.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecksUI(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHealthChecksUI(
                setupSettings: settings =>
                {
                    var healthChecks = new List<HealthCheckSetting>();

                    configuration.GetSection("HealthChecks").Bind(healthChecks);

                    foreach (var healthCheck in healthChecks)
                    {
                        settings.AddHealthCheckEndpoint(healthCheck.Name, healthCheck.Uri);
                    }
                }
            );
        }
    }
}