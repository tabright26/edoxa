// Filename: Startup.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright � 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Web.Spa.Extensions;
using eDoxa.Web.Spa.Infrastructure;

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
        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
            AppSettings = configuration.GetAppSettings<WebSpaAppSettings>();
        }

        public IConfiguration Configuration { get; }

        public IHostingEnvironment HostingEnvironment { get; }

        public WebSpaAppSettings AppSettings { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(AppSettings);

            //if (Configuration.GetValue<bool>("AzureKubernetesService:Enable"))
            //{
            //    services.AddDataProtection(
            //            options =>
            //            {
            //                options.ApplicationDiscriminator = Configuration["ApplicationDiscriminator"];
            //            }
            //        )
            //        .PersistKeysToRedis(ConnectionMultiplexer.Connect(Configuration.GetConnectionString(CustomConnectionStrings.Redis)), "data-protection");
            //}

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

            if (HostingEnvironment.IsDevelopment())
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

                    if (HostingEnvironment.IsDevelopment())
                    {
                        builder.UseReactDevelopmentServer("start");
                    }
                }
            );
        }
    }
}