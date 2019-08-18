﻿// Filename: IdentityDbContextFactory.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Seedwork.Security.Constants;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Identity.Api.Infrastructure.Data.Migrations
{
    internal sealed class IdentityDbContextFactory : IDesignTimeDbContextFactory<IdentityDbContext>
    {
        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

        public IdentityDbContext CreateDbContext(string[] args)
        {
            return new IdentityDbContext(
                new DbContextOptionsBuilder<IdentityDbContext>().UseSqlServer(
                        Configuration.GetConnectionString(CustomConnectionStrings.SqlServer),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name)
                    )
                    .Options
            );
        }
    }
}
