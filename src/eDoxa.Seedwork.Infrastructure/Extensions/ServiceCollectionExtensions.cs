﻿// Filename: ServiceCollectionExtensions.cs
// Date Created: 2019-04-30
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using eDoxa.Security;

using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace eDoxa.Seedwork.Infrastructure.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddDbContext<TDbContext>(this IServiceCollection services, IConfiguration configuration)
        where TDbContext : DbContext
        {
            services.AddDbContext<TDbContext>(
                options => options.UseSqlServer(
                    configuration.GetConnectionString(CustomConnectionStrings.SqlServer),
                    sqlServerOptions =>
                    {
                        sqlServerOptions.MigrationsAssembly(Assembly.GetAssembly(typeof(TDbContext)).GetName().Name);
                        sqlServerOptions.EnableRetryOnFailure(10, TimeSpan.FromSeconds(30), null);
                    }
                )
            );
        }
    }
}