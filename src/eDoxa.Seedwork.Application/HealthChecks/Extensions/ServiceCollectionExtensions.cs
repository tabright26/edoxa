// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using HealthChecks.UI.Configuration;

using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.HealthChecks.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddCustomHealthChecksUI(this IServiceCollection services)
        {
            var endpoints = new List<HealthCheckSetting>();

            var provider = services.BuildServiceProvider();

            var configuration = provider.GetRequiredService<IConfiguration>();

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
                        configuration.GetValue<int>("HealthChecks:MinimumSecondsBetweenFailureNotifications"));
                });
        }
    }
}
