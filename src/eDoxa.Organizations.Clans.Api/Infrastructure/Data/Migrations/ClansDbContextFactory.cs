// Filename: ClansDbContextFactory.cs
// Date Created: 2019-09-15
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Organizations.Clans.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Organizations.Clans.Api.Infrastructure.Data.Migrations
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
                        Configuration.GetConnectionString(Seedwork.Infrastructure.ConnectionStrings.SqlServer),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name))
                    .Options);
        }
    }
}
