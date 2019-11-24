// Filename: Startup.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Web.Status.Extensions;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Web.Status
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
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks().AddCheck("liveness", () => HealthCheckResult.Healthy());

            services.AddHealthChecksUI(Configuration);
        }

        public void Configure(IApplicationBuilder application, IWebHostEnvironment environment)
        {
            const string UIPath = "/status";

            if (environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UsePathBase(Configuration["ASPNETCORE_PATHBASE"]);

            application.UseHttpsRedirection();

            application.UseRouting();

            application.UseEndpoints(
                endpoints =>
                {
                    endpoints.MapHealthChecks(
                        "/liveness",
                        new HealthCheckOptions
                        {
                            Predicate = registration => registration.Name.Contains("liveness")
                        });

                    endpoints.MapHealthChecksUI(options => options.UIPath = UIPath);
                });

            application.UseStatusCodePagesWithRedirects(UIPath);
        }
    }
}
