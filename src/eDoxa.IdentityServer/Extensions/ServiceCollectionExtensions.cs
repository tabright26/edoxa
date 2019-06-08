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

using eDoxa.IdentityServer.Services;
using eDoxa.Seedwork.Application.Extensions;
using eDoxa.Seedwork.Security;

using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
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
    }
}
