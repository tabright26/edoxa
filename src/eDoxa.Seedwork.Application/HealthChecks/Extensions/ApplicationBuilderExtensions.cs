// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Builder;

namespace eDoxa.Seedwork.Application.HealthChecks.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCustomHealthCheckUI(this IApplicationBuilder application)
        {
            const string UIPath = "/status";

            application.UseRouting();

            application.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapCustomLivenessHealthChecks();

                    endpoints.MapHealthChecksUI(options => options.UIPath = UIPath);
                });

            application.UseStatusCodePagesWithRedirects(UIPath);
        }
    }
}
