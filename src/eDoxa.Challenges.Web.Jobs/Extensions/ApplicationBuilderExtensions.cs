// Filename: ApplicationBuilderExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using eDoxa.Challenges.Web.Jobs.Filters;

using Hangfire;

using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Challenges.Web.Jobs.Extensions
{
    public static class ApplicationBuilderExtensions
    {
        public static void UseCustomHangfireDashboard(this IApplicationBuilder application)
        {
            const string pathMatch = "/hangfire";

            application.UseHangfireDashboard(
                pathMatch,
                new DashboardOptions
                {
                    Authorization = new[] {new AllowAnonymousDashboardAuthorizationFilter()}
                });

            application.UseStatusCodePagesWithRedirects(pathMatch);
        }

        public static void UseHangfireRecurringJobs(this IApplicationBuilder application, Action<IRecurringJobManager> action)
        {
            action(application.ApplicationServices.GetRequiredService<IRecurringJobManager>());
        }
    }
}
