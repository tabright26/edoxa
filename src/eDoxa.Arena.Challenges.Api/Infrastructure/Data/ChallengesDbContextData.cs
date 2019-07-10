// Filename: ChallengesDbContextData.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data
{
    public sealed class ChallengesDbContextData : IDbContextData
    {
        private readonly ChallengesDbContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly IChallengeService _challengeService;

        public ChallengesDbContextData(ChallengesDbContext context, IHostingEnvironment environment, IChallengeService challengeService)
        {
            _context = context;
            _environment = environment;
            _challengeService = challengeService;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Challenges.Any())
                {
                    await _challengeService.FakeChallengesAsync(10, 23434503);
                }
            }
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
