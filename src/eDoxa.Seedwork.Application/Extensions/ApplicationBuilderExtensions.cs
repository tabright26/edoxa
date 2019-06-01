// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-04-27
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Application.Middlewares;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCustomExceptionHandler(this IApplicationBuilder application)
        {
            application.UseMiddleware<CustomExceptionHandlerMiddleware>();
        }

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

        public static void UseHealthChecksUI(this IApplicationBuilder application, string path)
        {
            application.UseHealthChecksUI(config => config.UIPath = path);

            application.UseStatusCodePagesWithRedirects(path);
        }
    }
}