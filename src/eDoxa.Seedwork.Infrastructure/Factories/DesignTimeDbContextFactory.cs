// Filename: DesignTimeDbContextFactory.cs
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

using eDoxa.Seedwork.Security.Constants;

using JetBrains.Annotations;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Infrastructure.Factories
{
    public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
    {
        protected abstract string BasePath { get; }

        protected abstract Assembly MigrationsAssembly { get; }

        protected DbContextOptions<TContext> Options
        {
            get
            {
                var builder = new DbContextOptionsBuilder<TContext>();

                builder.UseSqlServer(
                    Configuration.GetConnectionString(CustomConnectionStrings.SqlServer),
                    options => options.MigrationsAssembly(MigrationsAssembly.GetName().Name)
                );

                return builder.Options;
            }
        }

        private IConfiguration Configuration
        {
            get
            {
                var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");

                var builder = new ConfigurationBuilder();

                builder.SetBasePath(BasePath);

                builder.AddJsonFile("appsettings.json", false, true);

                builder.AddJsonFile($"appsettings.{environment}.json", true);

                builder.AddEnvironmentVariables();

                return builder.Build();
            }
        }

        [CanBeNull]
        public abstract TContext CreateDbContext(string[] args);
    }
}
