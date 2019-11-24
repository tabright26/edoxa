// Filename: NotificationsDbContextCleaner.cs
// Date Created: 2019-10-03
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Notifications.Infrastructure;
using eDoxa.Seedwork.Application;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Notifications.Api.Infrastructure.Data
{
    internal sealed class NotificationsDbContextCleaner : IDbContextCleaner
    {
        private readonly NotificationsDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public NotificationsDbContextCleaner(IWebHostEnvironment environment, NotificationsDbContext context)
        {
            _environment = environment;
            _context = context;
        }

        public Task CleanupAsync()
        {
            return Task.CompletedTask;
        }
    }
}
