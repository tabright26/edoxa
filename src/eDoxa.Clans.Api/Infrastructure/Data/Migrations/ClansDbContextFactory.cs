// Filename: ClansDbContextFactory.cs
// Date Created: 2019-09-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Clans.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Clans.Api.Infrastructure.Data.Migrations
{
    internal sealed class ClansDbContextFactory : IDesignTimeDbContextFactory<ClansDbContext>
    {
        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

        public ClansDbContext CreateDbContext(string[] args)
        {
            return new ClansDbContext(
                new DbContextOptionsBuilder<ClansDbContext>().UseSqlServer(
                        Configuration.GetSqlServerConnectionString(),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name))
                    .Options);
        }
    }
}
