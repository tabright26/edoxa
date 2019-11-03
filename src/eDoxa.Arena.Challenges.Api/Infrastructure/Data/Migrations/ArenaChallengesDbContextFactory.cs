// Filename: ArenaChallengesDbContextFactory.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Migrations
{
    internal sealed class ArenaChallengesDbContextFactory : IDesignTimeDbContextFactory<ArenaChallengesDbContext>
    {
        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

        public ArenaChallengesDbContext CreateDbContext(string[] args)
        {
            return new ArenaChallengesDbContext(
                new DbContextOptionsBuilder<ArenaChallengesDbContext>().UseSqlServer(
                        Configuration.GetSqlServerConnectionString(),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name)
                    )
                    .Options
            );
        }
    }
}
