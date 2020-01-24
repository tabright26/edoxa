// Filename: NotificationsDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Notifications.Api.Infrastructure.Data.Storage;
using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Api.Infrastructure.Data
{
    internal sealed class NotificationsDbContextSeeder : DbContextSeeder
    {
        public NotificationsDbContextSeeder(
            NotificationsDbContext context,
            IWebHostEnvironment environment,
            ILogger<NotificationsDbContextSeeder> logger
        ) : base(context, environment, logger)
        {
            Users = context.Set<User>();
        }

        private DbSet<User> Users { get; }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!Users.Any())
            {
                Users.AddRange(FileStorage.Users);

                await this.CommitAsync();

                Logger.LogInformation("The users being populated.");
            }
            else
            {
                Logger.LogInformation("The users already populated.");
            }
        }
    }
}
