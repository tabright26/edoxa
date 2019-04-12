// Filename: Startup.cs
// Date Created: 2019-04-12
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Monitoring.Extensions;
using eDoxa.Web.Spa.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.ReactDevelopmentServer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Web.Spa
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddOptions();

            services.AddHealthChecks(Configuration);

            //if (Configuration.GetValue<bool>("UseClusterEnvironment"))
            //{
            //    services.AddDataProtection(
            //                options =>
            //                {
            //                    options.ApplicationDiscriminator = Configuration["IdentityServer:Clients:Web:Spa:ClientId"];
            //                }
            //            )
            //            .PersistKeysToRedis(ConnectionMultiplexer.Connect(Configuration["Redis"]), "DataProtection-Keys");
            //}

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // In production, the React files will be served from this directory
            services.AddSpaStaticFiles(
                configuration =>
                {
                    configuration.RootPath = "ClientApp/build";
                }
            );
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder application, IHostingEnvironment env)
        {
            application.UseHealthChecks();

            if (env.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
            }
            else
            {
                application.UseExceptionHandler("/Error");
            }

            application.UseStaticFiles();
            application.UseSpaStaticFiles();

            application.UseMvcWithDefaultRoute();

            application.UseSpa(
                spa =>
                {
                    spa.Options.SourcePath = "ClientApp";

                    if (env.IsDevelopment())
                    {
                        spa.UseReactDevelopmentServer("start");
                    }
                }
            );
        }
    }
}