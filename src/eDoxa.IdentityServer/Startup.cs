// Filename: Startup.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IdentityServer.Data;
using eDoxa.IdentityServer.Extensions;
using eDoxa.IdentityServer.Models;
using eDoxa.IdentityServer.Services;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security;
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
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; }

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

            services.AddIdentity<User, Role>(options =>
                {
                    // Password settings
                    options.Password.RequireDigit = true;
                    options.Password.RequiredLength = 8;
                    options.Password.RequiredUniqueChars = 1;
                    options.Password.RequireLowercase = true;
                    options.Password.RequireNonAlphanumeric = true;
                    options.Password.RequireUppercase = true;

                    // Lockout settings
                    options.Lockout.AllowedForNewUsers = true;
                    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);
                    options.Lockout.MaxFailedAccessAttempts = 5;

                    // User settings
                    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789_";
                    options.User.RequireUniqueEmail = true;

                    // Claims settings
                    options.ClaimsIdentity.SecurityStampClaimType = CustomClaimTypes.SecurityStamp;

                    // SignIn settings
                    if (Environment.IsProduction())
                    {
                        options.SignIn.RequireConfirmedEmail = true;
                        options.SignIn.RequireConfirmedPhoneNumber = true;
                    }
                })
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4)
                .AddEntityFrameworkStores<IdentityServerDbContext>()
                .AddClaimsPrincipalFactory<CustomUserClaimsPrincipalFactory>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDataProtection(Configuration);

            var builder = services.AddIdentityServer(options =>
                {
                    options.IssuerUri = "null";

                    options.Authentication.CookieLifetime = TimeSpan.FromHours(2);

                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;
                    
                    options.UserInteraction.LoginUrl = "/Identity/Account/Login";
                    options.UserInteraction.LogoutUrl = "/Identity/Account/Logout";
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(Configuration))
                .AddProfileService<CustomProfileService>()
                .AddAspNetIdentity<User>();

            //.AddConfigurationStore(
            //    configurationStoreOptions =>
            //    {
            //        configurationStoreOptions.ConfigureDbContext = dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(
            //            Configuration.GetConnectionString("Sql"),
            //            sqlServerDbContextOptionsBuilder =>
            //            {
            //                sqlServerDbContextOptionsBuilder.MigrationsAssembly(Assembly.GetAssembly(typeof(IdentityDbContext)).GetName().Name);
            //            }
            //        );

            //        configurationStoreOptions.DefaultSchema = "idsrv";
            //    }
            //)
            //.AddOperationalStore(
            //    operationalStoreOptions =>
            //    {
            //        operationalStoreOptions.ConfigureDbContext = dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(
            //            Configuration.GetConnectionString("Sql"),
            //            sqlServerDbContextOptionsBuilder =>
            //            {
            //                sqlServerDbContextOptionsBuilder.MigrationsAssembly(Assembly.GetAssembly(typeof(IdentityDbContext)).GetName().Name);
            //            }
            //        );

            //        operationalStoreOptions.DefaultSchema = "idsrv";
            //    }
            //)

            if (Environment.IsDevelopment())
            {
                builder.AddCorsPolicyService<CustomCorsPolicyService>();
            }

            //services.AddAuthentication()
            //    .AddFacebook(
            //        facebookOptions =>
            //        {
            //            facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
            //            facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
            //        }
            //    )
            //    .AddTwitter(
            //        twitterOptions =>
            //        {
            //            twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
            //            twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
            //        }
            //    ).AddTwitch(
            //        twitchOptions =>
            //        {
            //            twitchOptions.ClientId = Configuration["Authentication:Twitch:ClientId"];
            //            twitchOptions.ClientSecret = Configuration["Authentication:Twitch:ClientSecret"];
            //        }
            //    )
            //    .AddBattleNet(
            //        battleNetOptions =>
            //        {
            //            battleNetOptions.ClientId = Configuration["Authentication:BattleNet:ClientId"];
            //            battleNetOptions.ClientSecret = Configuration["Authentication:BattleNet:ClientSecret"];
            //        }
            //    )
            //    .AddSteam();
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