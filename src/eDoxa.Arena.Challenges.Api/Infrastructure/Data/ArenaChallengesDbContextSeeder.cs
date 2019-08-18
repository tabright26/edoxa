// Filename: ArenaChallengesDbContextSeeder.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Arena.Challenges.Domain.Repositories;
using eDoxa.Arena.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data
{
    public sealed class ArenaChallengesDbContextSeeder : IDbContextSeeder
    {
        private readonly ArenaChallengesDbContext _context;
        private readonly IHostingEnvironment _environment;
        private readonly IChallengeRepository _challengeRepository;

        public ArenaChallengesDbContextSeeder(ArenaChallengesDbContext context, IHostingEnvironment environment, IChallengeRepository challengeRepository)
        {
            _context = context;
            _environment = environment;
            _challengeRepository = challengeRepository;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Challenges.Any())
                {
                    var challenges = ArenaChallengesStorage.TestChallenges.ToList();

                    _challengeRepository.Create(challenges);

                    await _challengeRepository.CommitAsync();
                }
            }
        }

        public void Cleanup()
        {
            if (!_environment.IsProduction())
            {
                _context.Challenges.RemoveRange(_context.Challenges);

                _context.SaveChanges();
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
