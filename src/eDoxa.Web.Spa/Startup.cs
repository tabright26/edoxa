// Filename: Startup.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Application.HealthChecks.Extensions;
using eDoxa.Seedwork.Application.HttpsPolicy.Extensions;
using eDoxa.Seedwork.Infrastructure.DataProtection.Extensions;
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

        public Startup(IConfiguration configuration, IWebHostEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IWebHostEnvironment Environment { get; }

        public WebSpaAppSettings AppSettings => Configuration.Get<WebSpaAppSettings>();

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions<WebSpaAppSettings>().Bind(Configuration).ValidateDataAnnotations();

            services.AddHealthChecks().AddCustomSelfCheck().AddCustomIdentityServer(AppSettings.Authority).AddCustomAzureKeyVault(Configuration);

            services.AddCustomDataProtection(Configuration, AppServices.WebSpa);

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddSpaStaticFiles(configuration => configuration.RootPath = "ClientApp/build");
        }

        public void Configure(IApplicationBuilder application)
        {
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
                    endpoints.MapCustomHealthChecks();
                });

            application.UseSpa(
                builder =>
                {
                    builder.Options.SourcePath = "ClientApp";

                    if (Environment.IsDevelopment())
                    {
                        builder.UseProxyToSpaDevelopmentServer(AppSettings.Client.Web.Endpoints.SpaUrl);
                    }
                });
        }
    }
}
