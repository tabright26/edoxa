// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-06-08
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Monitoring.Extensions;
using eDoxa.Seedwork.Security.Constants;

using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Identity.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddHealthChecks(this IServiceCollection services, IConfiguration configuration)
        {
            var healthChecks = services.AddHealthChecks();

            healthChecks.AddAzureKeyVault(configuration);

            healthChecks.AddSqlServer(configuration);

            healthChecks.AddIdentityServer(configuration);
        }

        public static void AddVersioning(this IServiceCollection services)
        {
            services.AddApiVersioning(
                options =>
                {
                    options.ApiVersionReader = new HeaderApiVersionReader(CustomHeaderNames.Version);
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                }
            );

            services.AddVersionedApiExplorer(options => options.GroupNameFormat = "'v'VV");
        }

        public static void AddIdentityCore<TUser, TRole, TContext>(this IServiceCollection services)
        where TUser : class
        where TRole : class
        where TContext : DbContext
        {
            services.AddIdentityCore<TUser>().AddRoles<TRole>().AddEntityFrameworkStores<TContext>();
        }
    }
}
