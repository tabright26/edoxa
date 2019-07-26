// Filename: Startup.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using eDoxa.Payment.Api.Extensions;
using eDoxa.Payment.Api.Infrastructure;
using eDoxa.Payment.Api.Providers.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Monitoring.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Payment.Api
{
    public sealed class Startup
    {
        public static Action<ContainerBuilder> ConfigureContainerBuilder = builder =>
        {
            // Required for testing.
        };

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<PaymentAppSettings>();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public PaymentAppSettings AppSettings { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddHealthChecks(AppSettings);

            services.AddProviders(Configuration);

            services.AddServiceBus(AppSettings);

            return CreateContainer(services);
        }
        
        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();

            if (HostingEnvironment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseProviders(Configuration);

            application.UseIntegrationEventSubscriptions();
        }

        private static IServiceProvider CreateContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<PaymentApiModule>();

            builder.Populate(services);

            ConfigureContainerBuilder(builder);
            
            return new AutofacServiceProvider(builder.Build());
        }
    }
}
