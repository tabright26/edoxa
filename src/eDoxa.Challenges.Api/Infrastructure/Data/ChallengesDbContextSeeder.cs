// Filename: ChallengesDbContextSeeder.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Seedwork.Infrastructure;

using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.Infrastructure.Data
{
    internal sealed class ChallengesDbContextSeeder : DbContextSeeder
    {
        private readonly ChallengesDbContext _context;
        private readonly IChallengeRepository _challengeRepository;

        public ChallengesDbContextSeeder(
            ChallengesDbContext context,
            IChallengeRepository challengeRepository,
            IHostingEnvironment environment,
            ILogger<ChallengesDbContextSeeder> logger
        ) : base(environment, logger)
        {
            _context = context;
            _challengeRepository = challengeRepository;
        }

        protected override async Task SeedDevelopmentAsync()
        {
            if (!_context.Challenges.Any())
            {
                var challenges = FileStorage.Challenges;

                _challengeRepository.Create(challenges);

                await _challengeRepository.CommitAsync();
            }
        }
    }
}
