// Filename: NotificationsDbContextCleaner.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Notifications.Infrastructure;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

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

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Users.RemoveRange(_context.Users);

                await _context.SaveChangesAsync();
            }
        }
    }
}
