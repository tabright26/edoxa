// Filename: NotificationsDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Notifications.Api.Infrastructure.Data.Storage;
using eDoxa.Notifications.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Notifications.Api.Infrastructure.Data
{
    internal sealed class NotificationsDbContextSeeder : DbContextSeeder
    {
        private readonly NotificationsDbContext _context;

        public NotificationsDbContextSeeder(
            NotificationsDbContext context,
            IWebHostEnvironment environment,
            ILogger<NotificationsDbContextSeeder> logger
        ) : base(environment, logger)
        {
            _context = context;
        }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!_context.Users.Any())
            {
                foreach (var user in FileStorage.Users)
                {
                    _context.Users.Add(user);
                }

                await _context.SaveChangesAsync();

                Logger.LogInformation("The users being populated.");
            }
            else
            {
                Logger.LogInformation("The users already populated.");
            }
        }
    }
}
