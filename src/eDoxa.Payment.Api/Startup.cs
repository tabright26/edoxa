// Filename: Startup.cs
// Date Created: 2019-07-05
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Autofac;

using eDoxa.Payment.Api.Extensions;
using eDoxa.Payment.Api.Infrastructure;
using eDoxa.Payment.Api.Providers.Extensions;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.ServiceBus;
using eDoxa.ServiceBus.Azure.Modules;
using eDoxa.ServiceBus.Modules;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Payment.Api
{
    public class Startup
    {
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
            services.Configure<ServiceBusOptions>(Configuration.GetSection("ServiceBus"));

            services.AddOptions();

            services.AddHealthChecks(AppSettings);

            services.AddProviders(Configuration);
        }

        public void ConfigureContainer(ContainerBuilder builder)
        {
            builder.RegisterModule<ServiceBusModule<Startup>>();

            builder.RegisterModule<AzureServiceBusModule>();

            builder.RegisterModule<PaymentApiModule>();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();

            if (HostingEnvironment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseProviders(Configuration);

            application.UseServiceBusSubscriber();
        }
    }
}
