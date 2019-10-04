// Filename: NotificationsDbContextFactory.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.IO;
using System.Reflection;

using eDoxa.Notifications.Infrastructure;

using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace eDoxa.Notifications.Api.Infrastructure.Data.Migrations
{
    internal sealed class NotificationsDbContextFactory : IDesignTimeDbContextFactory<NotificationsDbContext>
    {
        private static IConfiguration Configuration =>
            new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .AddEnvironmentVariables()
                .Build();

        public NotificationsDbContext CreateDbContext(string[] args)
        {
            return new NotificationsDbContext(
                new DbContextOptionsBuilder<NotificationsDbContext>().UseSqlServer(
                        Configuration.GetConnectionString(Seedwork.Infrastructure.ConnectionStrings.SqlServer),
                        builder => builder.MigrationsAssembly(Assembly.GetAssembly(typeof(Startup)).GetName().Name))
                    .Options);
        }
    }
}
