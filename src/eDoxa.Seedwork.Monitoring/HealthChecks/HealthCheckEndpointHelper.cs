// Filename: HealthCheckEndpointHelper.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.HealthChecks.Constants;

namespace eDoxa.Seedwork.Monitoring.HealthChecks
{
    internal class HealthCheckEndpointHelper
    {
        public static string Parse(string url)
        {
            return url + HealthCheckEndpoints.ReadinessUrl;
        }
    }
}
