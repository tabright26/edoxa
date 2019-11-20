// Filename: ChallengesDbContextFactory.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Migrations
{
    internal sealed class ChallengesDbContextFactory : IDesignTimeDbContextFactory<ChallengesDbContext>
    {
        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

        public ChallengesDbContext CreateDbContext(string[] args)
        {
            return new ChallengesDbContext(
                new DbContextOptionsBuilder<ChallengesDbContext>().UseSqlServer(
                        Configuration.GetSqlServerConnectionString(),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name))
                    .Options);
        }
    }
}
