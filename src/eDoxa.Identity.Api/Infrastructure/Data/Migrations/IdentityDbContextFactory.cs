// Filename: IdentityDbContextFactory.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;

using IdentityServer4.EntityFramework.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

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
                        Configuration.GetSqlServerConnectionString(),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup))!.GetName().Name))
                    .Options,
                new OptionsWrapper<OperationalStoreOptions>(new OperationalStoreOptions()));
        }
    }
}
