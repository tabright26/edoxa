// Filename: Startup.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IdentityModel.Tokens.Jwt;

using Autofac;

using eDoxa.Payment.Api.Extensions;
using eDoxa.Payment.Api.Infrastructure;
using eDoxa.Payment.Api.Providers.Extensions;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.ServiceBus.Modules;

using HealthChecks.UI.Client;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Payment.Api
{
    public class Startup
    {
        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
            JwtSecurityTokenHandler.DefaultInboundClaimTypeMap.Clear();
        }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<PaymentAppSettings>();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public PaymentAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddHealthChecks(AppSettings);

            services.AddProviders(Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule(new ServiceBusModule<Startup>(AppSettings));

            builder.RegisterModule<PaymentApiModule>();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseServiceBusSubscriber();

            if (HostingEnvironment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseProviders(Configuration);

            application.UseHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
            );
        }
    }
}
