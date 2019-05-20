// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Security.Abstractions;
using eDoxa.Security.Services;

using IdentityServer4.AccessTokenValidation;
using IdentityServer4.Models;

using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

using StackExchange.Redis;

namespace eDoxa.Security.Extensions
{
    public static class ServiceCollectionExtensions
    {
        // TODO: THIS IS NOT TESTED.
        public static void AddDataProtection(this IServiceCollection services, IConfiguration configuration)
        {
            if (configuration.GetValue<bool>("AzureKubernetesService:Enable"))
            {
                services.AddDataProtection(
                        options => { options.ApplicationDiscriminator = configuration["ApplicationDiscriminator"]; }
                    )
                    .PersistKeysToRedis(ConnectionMultiplexer.Connect(configuration.GetConnectionString(CustomConnectionStrings.Redis)), "data-protection");
            }
        }

        public static void AddUserInfoService(this IServiceCollection services)
        {
            services.AddSingleton<IUserInfoService, UserInfoService>();
        }

        public static void AddUserLoginInfoService(this IServiceCollection services)
        {
            services.AddSingleton<IUserLoginInfoService, UserLoginInfoService>();
        }

        public static void AddCookiePolicy(this IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
        }

        public static void AddCorsPolicy(this IServiceCollection services)
        {
            services.AddCors(
                options =>
                {
                    options.AddPolicy(CustomPolicies.CorsPolicy, builder => builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader().AllowCredentials());
                }
            );
        }

        public static void AddIdentity<TUser, TRole, TContext, TFactory>(this IServiceCollection services, IHostingEnvironment environment)
        where TUser : class
        where TRole : class
        where TContext : DbContext
        where TFactory : UserClaimsPrincipalFactory<TUser, TRole>
        {
            services.AddIdentity<TUser, TRole>(options =>
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
                    if (environment.IsProduction())
                    {
                        options.SignIn.RequireConfirmedEmail = true;
                        options.SignIn.RequireConfirmedPhoneNumber = true;
                    }
                })
                .AddClaimsPrincipalFactory<TFactory>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4);
        }

        public static void AddIdentityCore<TUser, TRole, TContext>(this IServiceCollection services)
        where TUser : class
        where TRole : class
        where TContext : DbContext
        {
            services.AddIdentityCore<TUser>()
                .AddRoles<TRole>()
                .AddEntityFrameworkStores<TContext>();
        }

        public static void AddIdentityServer<TUser>(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment)
        where TUser : IdentityUser<Guid>
        {
            var builder = services.AddIdentityServer(options =>
                {
                    options.IssuerUri = configuration.GetValue<string>("IdentityServer:Url");

                    options.Authentication.CookieLifetime = TimeSpan.FromHours(2);

                    options.Events.RaiseErrorEvents = true;
                    options.Events.RaiseInformationEvents = true;
                    options.Events.RaiseFailureEvents = true;
                    options.Events.RaiseSuccessEvents = true;

                    options.UserInteraction.LoginUrl = "/Account/Login";
                    options.UserInteraction.LogoutUrl = "/Account/Logout";
                })
                .AddDeveloperSigningCredential()
                .AddInMemoryPersistedGrants()
                .AddInMemoryIdentityResources(Config.GetIdentityResources())
                .AddInMemoryApiResources(Config.GetApiResources())
                .AddInMemoryClients(Config.GetClients(configuration))
                .AddProfileService<CustomProfileService<TUser>>()
                .AddAspNetIdentity<TUser>();

            if (environment.IsDevelopment())
            {
                builder.AddCorsPolicyService<CustomCorsPolicyService>();
            }
        }

        public static void AddIdentityServerAuthentication(
            this IServiceCollection services,
            IConfiguration configuration,
            IHostingEnvironment environment,
            ApiResource apiResource)
        {
            var authority = configuration.GetValue<string>("IdentityServer:Url");

            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                    {
                        options.ApiName = apiResource.Name;
                        options.Authority = authority;
                        options.RequireHttpsMetadata = environment.IsProduction();
                        options.ApiSecret = "secret";
                    }
                );
        }
    }
}