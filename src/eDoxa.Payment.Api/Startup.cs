// Filename: Startup.cs
// Date Created: 2019-07-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

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
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment Environment { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddHealthChecks(Configuration);

            services.AddProviders(Configuration);

            services.AddServiceBus(Configuration);

            return services.Build<ApiModule>();
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();

            if (Environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseProviders(Configuration);

            application.UseIntegrationEventSubscriptions();
        }
    }
}
