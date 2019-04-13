// Filename: DesignTimeDbContextFactory.cs
// Date Created: 2019-04-13
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Reflection;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Seedwork.Infrastructure.Factories
{
    public abstract class DesignTimeDbContextFactory<TContext> : IDesignTimeDbContextFactory<TContext>
    where TContext : DbContext
    {
        public abstract TContext CreateDbContext(string[] args);

        protected abstract string BasePath { get; }

        protected abstract Assembly MigrationsAssembly { get; }

        protected DbContextOptions<TContext> Options
        {
            get
            {
                var builder = new DbContextOptionsBuilder<TContext>();

                builder.UseSqlServer(Configuration.GetConnectionString("Sql"), options => options.MigrationsAssembly(MigrationsAssembly.GetName().Name));

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
    }
}