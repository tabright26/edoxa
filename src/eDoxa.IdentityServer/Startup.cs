// Filename: Startup.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.IdentityServer.Data;
using eDoxa.IdentityServer.Extensions;
using eDoxa.IdentityServer.Models;
using eDoxa.IdentityServer.Services;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.IdentityServer
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        private IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddHealthChecks(Configuration);

            services.AddEntityFrameworkSqlServer();

            services.AddDbContext<IdentityServerDbContext>(Configuration);

            services.AddIdentity<User, Role>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<IdentityServerDbContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDataProtection(Configuration);

            services.AddIdentityServer(options =>
                {
                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    options.IssuerUri = "null";
                    options.UserInteraction.LoginUrl = "/Identity/Account/Login";
                    options.UserInteraction.LogoutUrl = "/Identity/Account/Logout";
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(Configuration))
                .AddAspNetIdentity<User>()
                .AddCorsPolicyService<CustomCorsPolicyService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseHealthChecks();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseForwardedHeaders();
            app.UseCookiePolicy();

            app.UseIdentityServer();

            app.UseMvc(
                routes =>
                {
                    routes.MapRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                }
            );
        }
    }
}