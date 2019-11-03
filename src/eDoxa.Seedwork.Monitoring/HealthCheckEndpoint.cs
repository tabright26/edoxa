// Filename: HealthCheckEndpoint.cs
// Date Created: 2019-11-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Monitoring
{
    public static class HealthCheckEndpoint
    {
        public static string Parse(string url)
        {
            return url + "/health";
        }
    }
}
