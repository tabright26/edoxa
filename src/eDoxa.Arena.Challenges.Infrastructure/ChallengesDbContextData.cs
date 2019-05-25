// Filename: ChallengesDbContextData.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;
using System.Threading.Tasks;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.Services;
using eDoxa.Arena.Challenges.Services.Builders;
using eDoxa.Seedwork.Infrastructure.Abstractions;

using Microsoft.AspNetCore.Hosting;

namespace eDoxa.Arena.Challenges.Infrastructure
{
    public sealed class ChallengesDbContextData : IDbContextData
    {
        private readonly ChallengesDbContext _context;
        private readonly IFakeChallengeService _fakeChallengeService;
        private readonly IHostingEnvironment _environment;

        public ChallengesDbContextData(IHostingEnvironment environment, ChallengesDbContext context, IFakeChallengeService fakeChallengeService)
        {
            _environment = environment;
            _context = context;
            _fakeChallengeService = fakeChallengeService;
        }

        public async Task SeedAsync()
        {
            if (_environment.IsDevelopment())
            {
                if (!_context.Challenges.Any())
                {
                    //foreach (var challengeType in ChallengeType.GetAll())
                    //{
                    //    var builder = new FakeLeagueOfLegendsChallengeBuilder(challengeType);

                    //    await _fakeChallengeService.CreateChallenge(builder, true, true);
                    //}
                }
            }
        }
    }
}
