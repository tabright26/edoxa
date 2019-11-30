// Filename: HealthCheckEndpoints.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

namespace eDoxa.Seedwork.Monitoring.HealthChecks.Constants
{
    internal static class HealthCheckEndpoints
    {
        public const string LivenessName = "liveness";
        public const string LivenessUrl = "/" + LivenessName;
        public const string ReadinessName = "health";
        public const string ReadinessUrl = "/" + ReadinessName;
    }
}
