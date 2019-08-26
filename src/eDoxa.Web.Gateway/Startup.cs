﻿// Filename: Startup.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Web.Gateway.Extensions;
using eDoxa.Web.Gateway.Infrastructure;

using IdentityServer4.Models;

using Microsoft.ApplicationInsights.Extensibility.Implementation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

using static eDoxa.Seedwork.Security.IdentityServer.Resources.CustomApiResources;

namespace eDoxa.Web.Gateway
{
    public class Startup
    {
        static Startup()
        {
            TelemetryDebugWriter.IsTracingDisabled = true;
        }

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<WebGatewayAppSettings>();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public WebGatewayAppSettings AppSettings { get; set; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(AppSettings);

            services.AddCors(
                options =>
                {
                    options.AddPolicy("default", builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(_ => true));
                }
            );

            services.AddAuthentication(
                HostingEnvironment,
                AppSettings,
                new Dictionary<string, ApiResource>
                {
                    ["IdentityApiKey"] = IdentityApi,
                    ["CashierApiKey"] = CashierApi,
                    ["ArenaChallengesApiKey"] = ArenaChallengesApi
                }
            );

            services.AddOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();

            if (HostingEnvironment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseCors("default");

            application.UseOcelot().Wait();
        }
    }
}
