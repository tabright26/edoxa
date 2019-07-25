// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace eDoxa.Seedwork.Monitoring.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseHealthChecks(this IApplicationBuilder application)
        {
            application.UseHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
            );
        }
    }
}
