// Filename: Startup.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application.DevTools.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Monitoring;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Monitoring.HealthChecks.Extensions;
using eDoxa.Seedwork.Security.DataProtection.Extensions;
using eDoxa.Seedwork.Security.ForwardedHeaders.Extensions;
using eDoxa.Seedwork.Security.Hsts.Extensions;
using eDoxa.Web.Spa.Infrastructure;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
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

            services.AddCustomForwardedHeaders();

            services.AddHealthChecks()
                .AddCustomSelfCheck()
                .AddIdentityServer(AppSettings)
                .AddAzureKeyVault(Configuration)
                .AddUrlGroup(AppSettings.ChallengesWebGatewayUrl, AppNames.ChallengesWebGateway);

            services.AddCustomDataProtection(Configuration, AppNames.WebSpa);

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseForwardedHeaders();

            application.UseCustomMvcExceptionHandler();

            application.UseCustomHsts();

            application.UseCustomPathBase();

            application.UseHttpsRedirection();
            application.UseStaticFiles();
            application.UseSpaStaticFiles();

            application.UseRouting();

            application.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapConfigurationRoute<WebSpaAppSettings>();

                    endpoints.MapCustomHealthChecks();
                });

            application.UseSpa(
                builder =>
                {
                    builder.Options.SourcePath = "ClientApp";

                    if (HostingEnvironment.IsDevelopment())
                    {
                        builder.UseProxyToSpaDevelopmentServer(AppSettings.WebSpaClientUrl);
                    }
                });
        }
    }
}
