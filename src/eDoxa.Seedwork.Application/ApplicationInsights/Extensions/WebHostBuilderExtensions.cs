// Filename: WebHostBuilderExtensions.cs
// Date Created: 2019-12-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Application.ApplicationInsights.Extensions
{
    public static class WebHostBuilderExtensions
    {
        public static IWebHostBuilder UseCustomApplicationInsights(this IWebHostBuilder builder)
        {
            return builder.ConfigureServices(
                services =>
                {
                    services.AddApplicationInsightsTelemetry();
                    services.AddApplicationInsightsKubernetesEnricher();
                });
        }
    }
}
