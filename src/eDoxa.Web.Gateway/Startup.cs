// Filename: Startup.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Web.Gateway.Extensions;
using eDoxa.Web.Gateway.Infrastructure;

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

namespace eDoxa.Web.Gateway
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
            AppSettings = configuration.GetAppSettings<WebGatewayAppSettings>();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public WebGatewayAppSettings AppSettings { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCheck("liveness", () => HealthCheckResult.Healthy())
                .AddUrlGroup(AppSettings.Endpoints.IdentityUrl, "identityapi")
                .AddUrlGroup(AppSettings.Endpoints.CashierUrl, "cashierapi")
                .AddUrlGroup(AppSettings.Endpoints.PaymentUrl, "paymentapi")
                .AddUrlGroup(AppSettings.Endpoints.NotificationsUrl, "notificationsapi")
                .AddUrlGroup(AppSettings.Endpoints.ChallengesUrl, "challengesapi")
                .AddUrlGroup(AppSettings.Endpoints.GamesUrl, "gamesapi")
                .AddUrlGroup(AppSettings.Endpoints.ClansUrl, "clansapi")
                .AddUrlGroup(AppSettings.Endpoints.ChallengesAggregatorUrl, "challengesaggregator");

            services.AddCors(
                options =>
                {
                    options.AddPolicy("default", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(_ => true));
                }
            );

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
                    ["ChallengesAggregatorKey"] = ChallengesAggregator
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
