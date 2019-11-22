// Filename: ChallengesDbContextSeeder.cs
// Date Created: 2019-11-11
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Domain.Repositories;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Seedwork.Application;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

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
            IWebHostEnvironment environment,
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

                await _challengeRepository.CommitAsync(false);
            }
        }

        protected override async Task SeedProductionAsync()
        {
            if (!_context.Challenges.Any())
            {
                var scoring = new Scoring(
                    new Dictionary<string, float>
                    {
                        ["Kills"] = 4.5F,
                        ["Assists"] = 3.5F,
                        ["Deaths"] = -4F,
                        ["Minions"] = 0.04F,
                        ["TotalDamageDealt"] = 0.0009F,
                        ["TotalDamageTaken"] = 0.00125F,
                        ["VisionScore"] = 0.38F,
                        ["Win"] = 20F
                    });

                var timeline = new ChallengeTimeline(new UtcNowDateTimeProvider(), new ChallengeDuration(TimeSpan.FromDays(7)));

                var challengeId1 = ChallengeId.Parse("a3bc170c-de79-4144-baed-138c054bec5c");

                var challenge1 = new Challenge(
                    challengeId1,
                    new ChallengeName("FREE CHALLENGE 1"),
                    Game.LeagueOfLegends,
                    new BestOf(1),
                    new Entries(10),
                    timeline,
                    scoring);

                var challengeId2 = ChallengeId.Parse("d61ea4b2-a753-437d-9c88-073d83ae4a1e");

                var challenge2 = new Challenge(
                    challengeId2,
                    new ChallengeName("FREE CHALLENGE 2"),
                    Game.LeagueOfLegends,
                    new BestOf(1),
                    new Entries(10),
                    timeline,
                    scoring);

                var challengeId3 = ChallengeId.Parse("18523171-4aaa-434c-bad7-0dda1f6604b0");

                var challenge3 = new Challenge(
                    challengeId3,
                    new ChallengeName("FREE CHALLENGE 3"),
                    Game.LeagueOfLegends,
                    new BestOf(1),
                    new Entries(10),
                    new ChallengeTimeline(new UtcNowDateTimeProvider(), new ChallengeDuration(TimeSpan.FromDays(7))),
                    scoring);

                var challengeId4 = ChallengeId.Parse("b341220e-a158-4503-9799-cf33658a3571");

                var challenge4 = new Challenge(
                    challengeId4,
                    new ChallengeName("FREE CHALLENGE 4"),
                    Game.LeagueOfLegends,
                    new BestOf(1),
                    new Entries(10),
                    timeline,
                    scoring);

                var challengeId5 = ChallengeId.Parse("6a0658e9-4fd7-4857-9417-18513bb1486f");

                var challenge5 = new Challenge(
                    challengeId5,
                    new ChallengeName("FREE CHALLENGE 5"),
                    Game.LeagueOfLegends,
                    new BestOf(1),
                    new Entries(10),
                    timeline,
                    scoring);

                _challengeRepository.Create(
                    new List<IChallenge>
                    {
                        challenge1,
                        challenge2,
                        challenge3,
                        challenge4,
                        challenge5
                    });

                await _challengeRepository.CommitAsync(false);
            }
        }
    }
}
