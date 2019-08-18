// Filename: ArenaChallengesDbContextCleaner.cs
// Date Created: 2019-08-18
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data
{
    internal sealed class ArenaChallengesDbContextCleaner : IDbContextCleaner
    {
        private readonly ArenaChallengesDbContext _context;
        private readonly IHostingEnvironment _environment;

        public ArenaChallengesDbContextCleaner(ArenaChallengesDbContext context, IHostingEnvironment environment)
        {
            _context = context;
            _environment = environment;
        }

        public async Task CleanupAsync()
        {
            if (!_environment.IsProduction())
            {
                _context.Challenges.RemoveRange(_context.Challenges);

                await _context.SaveChangesAsync();
            }
        }
    }
}
