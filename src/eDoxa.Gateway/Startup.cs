// Filename: Startup.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Gateway.Extensions;
using eDoxa.Gateway.Infrastructure;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;

using HealthChecks.UI.Client;

using IdentityServer4.Models;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

using static eDoxa.Seedwork.Security.ApiResources;

namespace eDoxa.Gateway
{
    public class Startup
    {
        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
        }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<GatewayAppSettings>();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public GatewayAppSettings AppSettings { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck("liveness", () => HealthCheckResult.Healthy())
                .AddUrlGroup(AppSettings.Endpoints.IdentityUrl, AppNames.IdentityApi)
                .AddUrlGroup(AppSettings.Endpoints.CashierUrl, AppNames.CashierApi)
                .AddUrlGroup(AppSettings.Endpoints.PaymentUrl, AppNames.PaymentApi)
                .AddUrlGroup(AppSettings.Endpoints.NotificationsUrl, AppNames.NotificationsApi)
                .AddUrlGroup(AppSettings.Endpoints.ChallengesUrl, AppNames.ChallengesApi)
                .AddUrlGroup(AppSettings.Endpoints.GamesUrl, AppNames.GamesApi)
                .AddUrlGroup(AppSettings.Endpoints.ClansUrl, AppNames.ClansApi)
                .AddUrlGroup(AppSettings.Endpoints.ChallengesWebAggregatorUrl, AppNames.ChallengesWebAggregator);

            services.AddCors(
                options =>
                {
                    options.AddPolicy("default", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(_ => true));
                }
            );

            //TODO: Add production cors policy.

            //options.AddPolicy("AllowSubdomain",
            //    builder =>
            //    {
            //        builder.WithOrigins("https://*.example.com")
            //            .SetIsOriginAllowedToAllowWildcardSubdomains();
            //    });

            services.AddAuthentication(
                AppSettings,
                new Dictionary<string, ApiResource>
                {
                    ["IdentityApiKey"] = IdentityApi,
                    ["CashierApiKey"] = CashierApi,
                    ["PaymentApiKey"] = PaymentApi,
                    ["NotificationsApiKey"] = NotificationsApi,
                    ["ChallengesApiKey"] = ChallengesApi,
                    ["GamesApiKey"] = GamesApi,
                    ["ClansApiKey"] = ClansApi,
                    ["ChallengesWebAggregatorKey"] = ChallengesWebAggregator
                }
            );

            services.AddOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder application)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UsePathBase(Configuration["ASPNETCORE_PATHBASE"]);

            application.UseCors("default");

            //application.UseHttpsRedirection(); // TODO: To verify.
            //application.UseForwardedHeaders(); // TODO: To verify.

            application.UseHealthChecks(
                "/liveness",
                new HealthCheckOptions
                {
                    Predicate = registration => registration.Name.Contains("liveness")
                });

            application.UseHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
            );

            application.UseOcelot().Wait();
        }
    }
}
