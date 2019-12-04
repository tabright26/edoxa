// Filename: Startup.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Gateway.Extensions;
using eDoxa.Gateway.Infrastructure;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Monitoring.HealthChecks.Extensions;
using eDoxa.Seedwork.Security.Cors.Extensions;

using IdentityServer4.Models;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

using static eDoxa.Seedwork.Security.ApiResources;

namespace eDoxa.Gateway
{
    public sealed class Startup
    {
        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
        }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            AppSettings = configuration.GetAppSettings<GatewayAppSettings>();
        }

        public IConfiguration Configuration { get; }

        public GatewayAppSettings AppSettings { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddUrlGroup(AppSettings.Endpoints.IdentityUrl, AppNames.IdentityApi)
                .AddUrlGroup(AppSettings.Endpoints.CashierUrl, AppNames.CashierApi)
                .AddUrlGroup(AppSettings.Endpoints.PaymentUrl, AppNames.PaymentApi)
                .AddUrlGroup(AppSettings.Endpoints.NotificationsUrl, AppNames.NotificationsApi)
                .AddUrlGroup(AppSettings.Endpoints.ChallengesUrl, AppNames.ChallengesApi)
                .AddUrlGroup(AppSettings.Endpoints.GamesUrl, AppNames.GamesApi)
                .AddUrlGroup(AppSettings.Endpoints.ClansUrl, AppNames.ClansApi)
                .AddUrlGroup(AppSettings.Endpoints.ChallengesWebAggregatorUrl, AppNames.ChallengesWebAggregator)
                .AddUrlGroup(AppSettings.Endpoints.CashierWebAggregatorUrl, AppNames.CashierWebAggregator);

            services.AddCustomCors();

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
                    ["ChallengesWebAggregatorKey"] = ChallengesWebAggregator,
                    ["CashierWebAggregatorKey"] = CashierWebAggregator
                });

            services.AddOcelot(Configuration);
        }

        public async void Configure(IApplicationBuilder application)
        {
            application.UseCustomPathBase();

            application.UseRouting();
            application.UseCustomCors();

            application.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapCustomHealthChecks();
                });

            await application.UseOcelot();
        }
    }
}
