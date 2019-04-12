// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-04-12
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace eDoxa.Monitoring.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseHealthChecks(this IApplicationBuilder application)
        {
            application.UseHealthChecks(
                "/hc",
                new HealthCheckOptions
                {
                    Predicate = _ => true, ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
            );
        }

        public static void UseHealthChecksUI(this IApplicationBuilder application, string path)
        {
            application.UseHealthChecksUI(config => config.UIPath = path);

            application.UseStatusCodePagesWithRedirects(path);

            application.UseMvcWithDefaultRoute();
        }
    }
}