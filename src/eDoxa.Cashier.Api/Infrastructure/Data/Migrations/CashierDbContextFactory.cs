// Filename: CashierDbContextFactory.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Cashier.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Cashier.Api.Infrastructure.Data.Migrations
{
    internal sealed class CashierDbContextFactory : IDesignTimeDbContextFactory<CashierDbContext>
    {
        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

        public CashierDbContext CreateDbContext(string[] args)
        {
            return new CashierDbContext(
                new DbContextOptionsBuilder<CashierDbContext>().UseSqlServer(
                        Configuration.GetConnectionString(Seedwork.Infrastructure.ConnectionStrings.SqlServer),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name)
                    )
                    .Options
            );
        }
    }
}
