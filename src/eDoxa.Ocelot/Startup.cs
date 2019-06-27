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

using eDoxa.Monitoring.Extensions;
using eDoxa.Ocelot.Extensions;
using eDoxa.Seedwork.Security.Constants;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.IdentityServer.Resources;

using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
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
                    CustomPolicies.CorsPolicy,
                    builder => builder.AllowAnyMethod().AllowAnyHeader().AllowCredentials().SetIsOriginAllowed(_ => true)
                )
            );

            services.AddAuthentication(
                Configuration,
                Environment,
                new Dictionary<string, ApiResource>
                {
                    ["IdentityApiKey"] = CustomApiResources.Identity,
                    ["CashierApiKey"] = CustomApiResources.Cashier,
                    ["ArenaChallengesApiKey"] = CustomApiResources.ArenaChallenges,
                    ["WebAggregatorKey"] = CustomApiResources.Aggregator
                }
            );

            services.AddOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();

            if (Environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            application.UseCorsPolicy();

            application.UseOcelot().Wait();
        }
    }
}
