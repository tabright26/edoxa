// Filename: NotificationsDbContextSeeder.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Notifications.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Api.Infrastructure.Data
{
    internal sealed class NotificationsDbContextSeeder : IDbContextSeeder
    {
        private readonly NotificationsDbContext _context;
        private readonly ILogger<NotificationsDbContextSeeder> _logger;
        private readonly IHostingEnvironment _environment;

        public NotificationsDbContextSeeder(ILogger<NotificationsDbContextSeeder> logger, IHostingEnvironment environment, NotificationsDbContext context)
        {
            _logger = logger;
            _environment = environment;
            _context = context;
        }

        public Task SeedAsync()
        {
            return Task.CompletedTask;
        }
    }
}
