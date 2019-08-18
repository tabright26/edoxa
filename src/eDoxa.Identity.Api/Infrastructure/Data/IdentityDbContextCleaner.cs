// Filename: IdentityDbContextCleaner.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Identity.Api.Infrastructure.Data
{
    internal sealed class IdentityDbContextCleaner : IDbContextCleaner
    {
        private readonly IHostingEnvironment _environment;
        private readonly IdentityDbContext _context;

        public IdentityDbContextCleaner(IHostingEnvironment environment, IdentityDbContext context)
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
