﻿// Filename: Startup.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.Infrastructure;
using eDoxa.IdentityServer.Extensions;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.IdentityServer
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

            services.AddCookiePolicy();
            
            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<IdentityDbContext>(Configuration);

            services.AddDataProtection(Configuration);

            services.AddIdentity<User, Role, IdentityDbContext>(Environment);

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddIdentityServer<User>(Configuration, Environment);
        }

        public void Configure(IApplicationBuilder application)
        {
            application.UseHealthChecks();

            if (Environment.IsDevelopment())
            {
                application.UseDeveloperExceptionPage();
                application.UseDatabaseErrorPage();
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