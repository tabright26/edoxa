// Filename: ClansDbContextCleaner.cs
// Date Created: 2019-09-15
//
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Organizations.Clans.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Organizations.Clans.Api.Infrastructure.Data
{
    internal sealed class ClansDbContextCleaner : IDbContextCleaner
    {
        private readonly ClansDbContext _context;
        private readonly IHostingEnvironment _environment;

        public ClansDbContextCleaner(IHostingEnvironment environment, ClansDbContext context)
        {
            _environment = environment;
            _context = context;
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Clans.RemoveRange(_context.Clans);

                await _context.SaveChangesAsync();
            }
        }
    }
}
