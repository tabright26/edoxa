// Filename: EnpointRoutingBuilderExtensions.cs
// Date Created: 2019-11-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Monitoring.HealthChecks.Constants;

using HealthChecks.UI.Client;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Routing;

namespace eDoxa.Seedwork.Monitoring.HealthChecks.Extensions
{
    public static class EnpointRoutingBuilderExtensions
    {
        public static void MapCustomHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            endpoints.MapCustomLivenessHealthChecks();

            endpoints.MapCustomReadinessHealthChecks();
        }

        public static IEndpointConventionBuilder MapCustomLivenessHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHealthChecks(
                HealthCheckEndpoints.LivenessUrl,
                new HealthCheckOptions
                {
                    Predicate = registration => registration.Name.Contains(HealthCheckEndpoints.LivenessName)
                });
        }

        public static IEndpointConventionBuilder MapCustomReadinessHealthChecks(this IEndpointRouteBuilder endpoints)
        {
            return endpoints.MapHealthChecks(
                HealthCheckEndpoints.ReadinessUrl,
                new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
        }
    }
}
