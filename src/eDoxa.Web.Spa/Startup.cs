// Filename: Startup.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Web.Spa.Infrastructure;

using HealthChecks.UI.Client;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Web.Spa
{
    public sealed class Startup
    {
        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
        }

        public Startup(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<WebSpaAppSettings>();
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment HostingEnvironment { get; }

        public WebSpaAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<WebSpaAppSettings>(Configuration);

            services.AddHealthChecks()
                .AddCheck("liveness", () => HealthCheckResult.Healthy())
                .AddIdentityServer(AppSettings)
                .AddAzureKeyVault(Configuration)
                .AddUrlGroup(AppSettings.ChallengesWebGatewayUrl, AppNames.ChallengesWebGateway);

            services.AddDataProtection(Configuration, AppNames.WebSpa);

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddMvc();

            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");
        }

        public void Configure(IApplicationBuilder application)
        {
            if (HostingEnvironment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                application.UseHsts();
            }

            application.UsePathBase(Configuration["ASPNETCORE_PATHBASE"]);

            application.UseStaticFiles();
            application.UseSpaStaticFiles();

            application.UseMvcWithDefaultRoute();

            application.UseSpa(
                builder =>
                {
                    builder.Options.SourcePath = "ClientApp";

                    if (HostingEnvironment.IsDevelopment())
                    {
                        builder.UseProxyToSpaDevelopmentServer(AppSettings.WebSpaClientUrl);
                    }
                });

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
                });
        }
    }
}
