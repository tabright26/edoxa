﻿// Filename: ChallengesDbContextCleaner.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Threading.Tasks;

using eDoxa.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Challenges.Api.Infrastructure.Data
{
    internal sealed class ChallengesDbContextCleaner : IDbContextCleaner
    {
        private readonly ChallengesDbContext _context;
        private readonly IHostingEnvironment _environment;

        public ChallengesDbContextCleaner(ChallengesDbContext context, IHostingEnvironment environment)
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