// Filename: NotificationsDbContextCleaner.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Notifications.Domain.AggregateModels.UserAggregate;
using eDoxa.Notifications.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Api.Infrastructure.Data
{
    internal sealed class NotificationsDbContextCleaner : DbContextCleaner
    {
        public NotificationsDbContextCleaner(
            NotificationsDbContext context,
            IWebHostEnvironment environment,
            ILogger<NotificationsDbContextCleaner> logger
        ) : base(context, environment, logger)
        {
            Users = context.Set<User>();
        }

        private DbSet<User> Users { get; }

        protected override void Cleanup()
        {
            Users.RemoveRange(Users);
        }
    }
}
