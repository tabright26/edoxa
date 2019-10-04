// Filename: ArenaChallengesDbContextSeeder.cs
// Date Created: 2019-09-29
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
using Microsoft.Extensions.Logging;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data
{
    internal sealed class ArenaChallengesDbContextSeeder : DbContextSeeder
    {
        private readonly ArenaChallengesDbContext _context;
        private readonly IChallengeRepository _challengeRepository;

        public ArenaChallengesDbContextSeeder(
            ArenaChallengesDbContext context,
            IChallengeRepository challengeRepository,
            IHostingEnvironment environment,
            ILogger<ArenaChallengesDbContextSeeder> logger
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
