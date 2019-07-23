// Filename: Startup.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Autofac;
using Autofac.Extensions.DependencyInjection;

using eDoxa.IntegrationEvents.Extensions;
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
    public class Startup
    {
        public static Action<ContainerBuilder> ConfigureContainer = builder =>
        {
            // Required for testing.
        };

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddHealthChecks(Configuration);

            services.AddProviders(Configuration);

            services.AddServiceBus(Configuration);

            return this.BuildModule(services);
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

        private IServiceProvider CreateContainer(IServiceCollection services)
        {
            var builder = new ContainerBuilder();

            builder.RegisterModule<PaymentApiModule>();

            ConfigureContainer(builder);

            builder.Populate(services);

            return new AutofacServiceProvider(builder.Build());
        }

        // TODO: Required by integration and functional tests.
        protected virtual IServiceProvider BuildModule(IServiceCollection services)
        {
            return services.Build<PaymentApiModule>();
        }
    }
}
