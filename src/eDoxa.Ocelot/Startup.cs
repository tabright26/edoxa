// Filename: Startup.cs
// Date Created: 2019-06-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Ocelot.Extensions;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.IdentityServer.Resources;

using HealthChecks.UI.Client;

using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using Ocelot.DependencyInjection;
using Ocelot.Middleware;

namespace eDoxa.Ocelot
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(Configuration);

            services.AddCors(
                options => options.AddPolicy(
                    "CorsPolicy",
                    builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(host => true)
                )
            );

            services.AddAuthentication(
                Configuration,
                Environment,
                new Dictionary<string, ApiResource>
                {
                    ["IdentityApiKey"] = CustomApiResources.Identity,
                    ["CashierApiKey"] = CustomApiResources.Cashier,
                    ["ArenaChallengesApiKey"] = CustomApiResources.ArenaChallenges
                }
            );

            services.AddOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks(
                "/health",
                new HealthCheckOptions
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                }
            );

            if (Environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseCors("CorsPolicy");

            application.UseOcelot().Wait();
        }
    }
}
