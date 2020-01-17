// Filename: IdentityDbContextFactory.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;

using IdentityServer4.EntityFramework.Options;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;

using Moq;

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
            var mock = new Mock<IOptionsSnapshot<OperationalStoreOptions>>();

            mock.Setup(snapshot => snapshot.Value).Returns(new OperationalStoreOptions());

            return new IdentityDbContext(
                new DbContextOptionsBuilder<IdentityDbContext>().UseSqlServer(
                        Configuration.GetSqlServerConnectionString(),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup))!.GetName().Name))
                    .Options,
                mock.Object);
        }
    }
}
