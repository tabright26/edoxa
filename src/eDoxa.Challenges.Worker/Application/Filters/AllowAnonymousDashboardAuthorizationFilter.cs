// Filename: AllowAnonymousDashboardAuthorizationFilter.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Hangfire.Dashboard;

namespace eDoxa.Challenges.Worker.Application.Filters
{
    internal sealed class AllowAnonymousDashboardAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
