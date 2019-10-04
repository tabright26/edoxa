// Filename: NotificationsDbContextSeeder.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Notifications.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Api.Infrastructure.Data
{
    internal sealed class NotificationsDbContextSeeder : DbContextSeeder
    {
        private readonly NotificationsDbContext _context;

        public NotificationsDbContextSeeder(NotificationsDbContext context, IHostingEnvironment environment, ILogger<NotificationsDbContextSeeder> logger) : base(environment, logger)
        {
            _context = context;
        }
    }
}
