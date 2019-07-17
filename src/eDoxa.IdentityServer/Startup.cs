// Filename: Startup.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using AutoMapper;

using eDoxa.IdentityServer.Extensions;
using eDoxa.IntegrationEvents.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security;
using eDoxa.Seedwork.Security.Extensions;
using eDoxa.Seedwork.Security.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.IdentityServer
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

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(Configuration);

            services.AddCookiePolicy();

            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<IdentityDbContext>(Configuration, Assembly.GetAssembly(typeof(Startup)));

            services.AddDataProtection(Configuration);

            services.AddIdentity(Environment);

            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentityServer<UserModel>(Configuration, Environment);

            services.AddServiceBus(Configuration);

            return services.Build();
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
                application.UseExceptionHandler("/Home/Error");
                application.UseHsts();
            }

            application.UseHttpsRedirection();
            application.UseStaticFiles();
            application.UseForwardedHeaders();
            application.UseCookiePolicy();

            application.UseIdentityServer();

            application.UseMvc(
                routes =>
                {
                    routes.MapRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                }
            );
        }
    }
}
