// Filename: Startup.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Web.Spa.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Application.Extensions;

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

            services.AddDataProtection(Configuration);

            services.AddAntiforgery(options => options.HeaderName = "X-XSRF-TOKEN");

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddSpaStaticFiles(
                configuration =>
                {
                    configuration.RootPath = "ClientApp/build";
                }
            );
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();

            if (Environment.IsDevelopment())
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
                builder =>
                {
                    builder.Options.SourcePath = "ClientApp";

                    if (Environment.IsDevelopment())
                    {
                        builder.UseReactDevelopmentServer("start");
                    }
                }
            );
        }
    }
}