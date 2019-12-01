// Filename: PaymentDbContextFactory.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Payment.Infrastructure;
using eDoxa.Seedwork.Infrastructure.Extensions;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Payment.Api.Infrastructure.Data.Migrations
{
    internal sealed class PaymentDbContextFactory : IDesignTimeDbContextFactory<PaymentDbContext>
    {
        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

        public PaymentDbContext CreateDbContext(string[] args)
        {
            return new PaymentDbContext(
                new DbContextOptionsBuilder<PaymentDbContext>().UseSqlServer(
                        Configuration.GetSqlServerConnectionString(),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup))!.GetName().Name))
                    .Options);
        }
    }
}
