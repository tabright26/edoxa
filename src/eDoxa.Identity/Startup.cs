// Filename: Startup.cs
// Date Created: 2019-04-14
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using eDoxa.Autofac.Extensions;
using eDoxa.AutoMapper.Extensions;
using eDoxa.Identity.Application;
using eDoxa.Identity.Areas.Identity;
using eDoxa.Identity.Domain.AggregateModels.RoleAggregate;
using eDoxa.Identity.Domain.AggregateModels.UserAggregate;
using eDoxa.Identity.DTO.Factories;
using eDoxa.Identity.Extensions;
using eDoxa.Identity.Infrastructure;
using eDoxa.Monitoring.Extensions;
using eDoxa.Security.Extensions;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.ServiceBus;
using eDoxa.ServiceBus.Extensions;
using eDoxa.Swagger.Extensions;

using IdentityServer4.Configuration;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity
{
    public sealed class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        private IHostingEnvironment Environment { get; }

        private IConfiguration Configuration { get; }

        public IServiceProvider ConfigureServices(IServiceCollection services)
        {
            services.AddHealthChecks(Configuration);

            services.AddVersioning(new ApiVersion(1, 0));

            services.AddDbContext<IntegrationEventLogDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("Sql"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(IdentityDbContext)).GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }
                )
            );

            services.AddDbContext<IdentityDbContext>(
                options => options.UseSqlServer(
                    Configuration.GetConnectionString("Sql"),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(IdentityDbContext)).GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }
                )
            );

            services.AddIdentity<User, Role>(
                        options =>
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

                            // SignIn settings
                            if (Environment.IsProduction())
                            {
                                options.SignIn.RequireConfirmedEmail = true;
                                options.SignIn.RequireConfirmedPhoneNumber = true;
                            }
                        }
                    )
                    .AddDefaultTokenProviders()
                    .AddDefaultUI(UIFramework.Bootstrap4)
                    .AddEntityFrameworkStores<IdentityDbContext>()
                    .AddClaimsPrincipalFactory<UserClaimsPrincipalFactory>();

            services.AddAutoMapper(IdentityMapperFactory.Instance);

            services.AddMvcWithApiBehavior();

            services.AddSwagger(Configuration, Environment, Assembly.GetExecutingAssembly());

            services.AddDataProtection(Configuration);

            services.AddServiceBus(Configuration);

            services.AddEventBus(Configuration);

            services.AddIdentityServer(
                        options =>
                        {
                            options.Authentication.CookieLifetime = TimeSpan.FromHours(2);
                            options.Events.RaiseErrorEvents = true;
                            options.Events.RaiseInformationEvents = true;
                            options.Events.RaiseFailureEvents = true;
                            options.Events.RaiseSuccessEvents = true;
                            options.IssuerUri = Configuration["IdentityServer:IssuerUrl"];

                            options.UserInteraction = new UserInteractionOptions
                            {
                                LoginUrl = Configuration["IdentityServer:UserInteraction:LoginUrl"],
                                LogoutUrl = Configuration["IdentityServer:UserInteraction:LogoutUrl"],
                                ErrorUrl = Configuration["IdentityServer:UserInteraction:ErrorUrl"],
                                ConsentUrl = Configuration["IdentityServer:UserInteraction:ConsentUrl"]
                            };
                        }
                    )
                    .AddDeveloperSigningCredential()
                    .AddAspNetIdentity<User>()
                    .AddConfigurationStore(
                        configurationStoreOptions =>
                        {
                            configurationStoreOptions.ConfigureDbContext = dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(
                                Configuration.GetConnectionString("Sql"),
                                sqlServerDbContextOptionsBuilder =>
                                {
                                    sqlServerDbContextOptionsBuilder.MigrationsAssembly(Assembly.GetAssembly(typeof(IdentityDbContext)).GetName().Name);
                                }
                            );

                            configurationStoreOptions.DefaultSchema = "idsrv";
                        }
                    )
                    .AddOperationalStore(
                        operationalStoreOptions =>
                        {
                            operationalStoreOptions.ConfigureDbContext = dbContextOptionsBuilder => dbContextOptionsBuilder.UseSqlServer(
                                Configuration.GetConnectionString("Sql"),
                                sqlServerDbContextOptionsBuilder =>
                                {
                                    sqlServerDbContextOptionsBuilder.MigrationsAssembly(Assembly.GetAssembly(typeof(IdentityDbContext)).GetName().Name);
                                }
                            );

                            operationalStoreOptions.DefaultSchema = "idsrv";
                        }
                    )
                    .AddProfileService<ProfileService>();

            services.AddAuthentication()
                    .AddFacebook(
                        facebookOptions =>
                        {
                            facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                            facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                        }
                    )
                    .AddTwitter(
                        twitterOptions =>
                        {
                            twitterOptions.ConsumerKey = Configuration["Authentication:Twitter:ConsumerKey"];
                            twitterOptions.ConsumerSecret = Configuration["Authentication:Twitter:ConsumerSecret"];
                        }
                    );

            //.AddTwitch(
            //    twitchOptions =>
            //    {
            //        twitchOptions.ClientId = this.Configuration["Authentication:Twitch:ClientId"];
            //        twitchOptions.ClientSecret = this.Configuration["Authentication:Twitch:ClientSecret"];
            //    }
            //)
            //.AddBattleNet(
            //    battleNetOptions =>
            //    {
            //        battleNetOptions.ClientId = this.Configuration["Authentication:BattleNet:ClientId"];
            //        battleNetOptions.ClientSecret = this.Configuration["Authentication:BattleNet:ClientSecret"];
            //    }
            //)
            //.AddSteam();

            return services.Build<ApplicationModule>();
        }

        public void Configure(IApplicationBuilder application, IApiVersionDescriptionProvider provider)
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

            application.UseIdentityServer();

            //application.UseHttpsRedirection();
            application.UseStaticFiles();
            application.UseForwardedHeaders();

            application.UseSwagger(Configuration, Environment, provider);

            application.UseMvc(
                routes =>
                {
                    routes.MapRoute("area", "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                    routes.MapRoute("default", "{controller=Home}/{action=Index}/{id?}");
                }
            );

            application.UseIntegrationEventSubscriptions();
        }
    }
}