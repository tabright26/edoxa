// Filename: ArenaChallengesDbContextSeeder.cs
// Date Created: 2019-08-18
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
    internal sealed class ArenaChallengesDbContextSeeder : IDbContextSeeder
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
                    var challenges = FileStorage.Challenges;

                    _challengeRepository.Create(challenges);

                    await _challengeRepository.CommitAsync();
                }
            }
        }
    }
}
