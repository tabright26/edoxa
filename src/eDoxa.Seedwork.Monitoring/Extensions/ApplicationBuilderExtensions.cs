// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-06-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

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
