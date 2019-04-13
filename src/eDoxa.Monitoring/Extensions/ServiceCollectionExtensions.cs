// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-13
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
            var endpoints = new List<HealthCheckSetting>();

            configuration.GetSection("HealthChecks:Endpoints").Bind(endpoints);

            services.AddHealthChecksUI(
                setupSettings: settings =>
                {
                    foreach (var endpoint in endpoints)
                    {
                        settings.AddHealthCheckEndpoint(endpoint.Name, endpoint.Uri);
                    }

                    settings.SetEvaluationTimeInSeconds(configuration.GetValue<int>("HealthChecks:EvaluationTimeInSeconds"));

                    settings.SetMinimumSecondsBetweenFailureNotifications(
                        configuration.GetValue<int>("HealthChecks:MinimumSecondsBetweenFailureNotifications")
                    );
                }
            );
        }
    }
}