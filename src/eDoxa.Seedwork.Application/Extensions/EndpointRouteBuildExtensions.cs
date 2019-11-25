// Filename: EndpointRouteBuildExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Text.Json;

using eDoxa.Seedwork.Monitoring.AppSettings;

using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Routing;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Options;

namespace eDoxa.Seedwork.Application.Extensions
{
    public static class EndpointRouteBuildExtensions
    {
        private const string Pattern = "/api/_configuration";

        public static void MapConfigurationRoute<T>(this IEndpointRouteBuilder endpoints, ApiResource? apiResource = null)
        where T : class, new()
        {
            var environment = endpoints.ServiceProvider.GetRequiredService<IWebHostEnvironment>();

            if (environment.IsDevelopment())
            {
                var options = endpoints.ServiceProvider.GetRequiredService<IOptions<T>>();

                var value = options.Value;

                if (apiResource != null && value is IHasApiResourceAppSettings appSettings)
                {
                    appSettings.ApiResource = apiResource;
                }

                endpoints.MapGet(
                    Pattern,
                    async context =>
                    {
                        await context.Response.WriteAsync(
                            JsonSerializer.Serialize(
                                value,
                                new JsonSerializerOptions
                                {
                                    WriteIndented = true
                                }));
                    });
            }
        }
    }
}
