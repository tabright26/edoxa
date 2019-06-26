// Filename: Startup.cs
// Date Created: 2019-06-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Ocelot.Extensions;

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
            services.AddCors(
                options =>
                {
                    options.AddPolicy("CorsPolicy", builder => builder.AllowAnyMethod().AllowAnyHeader().SetIsOriginAllowed(host => true).AllowCredentials());
                }
            );

            services.AddAuthentication()
                .AddIdentityApiAuthentication("IdentityApiKey", Configuration["IdentityServer:Url"])
                .AddCashierApiAuthentication("CashierApiKey", Configuration["IdentityServer:Url"])
                .AddArenaChallengesApiAuthentication("ArenaChallengesApiKey", Configuration["IdentityServer:Url"]);

            services.AddOcelot(Configuration);
        }

        public void Configure(IApplicationBuilder application)
        {
            if (Environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }

            //application.UseHealthChecks(
            //    "/health",
            //    new HealthCheckOptions
            //    {
            //        Predicate = _ => true,
            //        ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
            //    }
            //);

            application.UseCors("CorsPolicy");

            application.UseOcelot().Wait();
        }
    }
}
