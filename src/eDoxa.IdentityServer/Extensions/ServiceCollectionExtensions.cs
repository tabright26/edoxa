// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-01
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.IdentityServer.Infrastructure;
using eDoxa.IdentityServer.Infrastructure.Services;
using eDoxa.Seedwork.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Constants;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.IdentityServer.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecks = services.AddHealthChecks();

            healthChecks.AddAzureKeyVault(configuration);

            healthChecks.AddSqlServer(configuration);

            healthChecks.AddRedis(configuration);
        }

        public static void AddIdentity<TUser, TRole, TContext, TFactory>(this IServiceCollection services, IHostingEnvironment environment)
        where TUser : class
        where TRole : class
        where TContext : DbContext
        where TFactory : UserClaimsPrincipalFactory<TUser, TRole>
        {
            services.AddIdentity<TUser, TRole>(
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

                        // Claims settings
                        options.ClaimsIdentity.SecurityStampClaimType = CustomClaimTypes.SecurityStamp;

                        // SignIn settings
                        if (environment.IsProduction())
                        {
                            options.SignIn.RequireConfirmedEmail = true;
                            options.SignIn.RequireConfirmedPhoneNumber = true;
                        }
                    }
                )
                .AddClaimsPrincipalFactory<TFactory>()
                .AddEntityFrameworkStores<TContext>()
                .AddDefaultTokenProviders()
                .AddDefaultUI(UIFramework.Bootstrap4);
        }

        public static void AddIdentityServer<TUser>(this IServiceCollection services, IConfiguration configuration, IHostingEnvironment environment)
        where TUser : IdentityUser<Guid>
        {
            var builder = services.AddIdentityServer(
                    options =>
                    {
                        options.IssuerUri = configuration.GetValue<string>("IdentityServer:Url");

                        options.Authentication.CookieLifetime = TimeSpan.FromHours(2);

                        options.Events.RaiseErrorEvents = true;
                        options.Events.RaiseInformationEvents = true;
                        options.Events.RaiseFailureEvents = true;
                        options.Events.RaiseSuccessEvents = true;

                        options.UserInteraction.LoginUrl = "/Account/Login";
                        options.UserInteraction.LogoutUrl = "/Account/Logout";
                    }
                )
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
    }
}
