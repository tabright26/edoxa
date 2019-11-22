// Filename: GamesDbContextCleaner.cs
// Date Created: 2019-10-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Games.Infrastructure;
using eDoxa.Seedwork.Application;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting;

namespace eDoxa.Games.Api.Infrastructure.Data
{
    internal sealed class GamesDbContextCleaner : IDbContextCleaner
    {
        private readonly GamesDbContext _context;
        private readonly IWebHostEnvironment _environment;

        public GamesDbContextCleaner(GamesDbContext context, IWebHostEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                //_context.Challenges.RemoveRange(_context.Challenges);

                await _context.SaveChangesAsync();
            }
        }
    }
}
