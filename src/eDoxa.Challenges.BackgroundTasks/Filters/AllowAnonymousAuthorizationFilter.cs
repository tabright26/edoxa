// Filename: AllowAnonymousAuthorizationFilter.cs
// Date Created: 2019-11-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Hangfire.Dashboard;

namespace eDoxa.Challenges.BackgroundTasks.Filters
{
    public sealed class AllowAnonymousAuthorizationFilter : IDashboardAuthorizationFilter
    {
        public bool Authorize(DashboardContext context)
        {
            return true;
        }
    }
}
