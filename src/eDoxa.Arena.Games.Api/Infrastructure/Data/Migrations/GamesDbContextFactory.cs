﻿// Filename: GamesDbContextFactory.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Arena.Games.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Arena.Games.Api.Infrastructure.Data.Migrations
{
    internal sealed class GamesDbContextFactory : IDesignTimeDbContextFactory<GamesDbContext>
    {
        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

        public GamesDbContext CreateDbContext(string[] args)
        {
            return new GamesDbContext(
                new DbContextOptionsBuilder<GamesDbContext>().UseSqlServer(
                        Configuration.GetConnectionString(Seedwork.Infrastructure.ConnectionStrings.SqlServer),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name))
                    .Options);
        }
    }
}