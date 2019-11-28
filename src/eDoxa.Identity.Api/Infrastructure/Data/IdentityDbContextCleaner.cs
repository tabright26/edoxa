// Filename: IdentityDbContextCleaner.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Identity.Infrastructure;
using eDoxa.Seedwork.Application;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    internal sealed class IdentityDbContextCleaner : IDbContextCleaner
    {
        private readonly IWebHostEnvironment _environment;
        private readonly IdentityDbContext _context;

        public IdentityDbContextCleaner(IWebHostEnvironment environment, IdentityDbContext context)
        {
            _environment = environment;
            _context = context;
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Users.RemoveRange(_context.Users);

                _context.Roles.RemoveRange(_context.Roles);

                await _context.SaveChangesAsync();
            }
        }
    }
}
