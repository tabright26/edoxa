// Filename: ChallengesDbContextSeeder.cs
// Date Created: 2019-11-25
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
using eDoxa.Seedwork.Application.SqlServer.Abstractions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

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
                        ["Deaths"] = -4F,
                        ["Assists"] = 3.5F,
                        ["TotalDamageDealtToChampions"] = 0.0009F,
                        ["TotalDamageTaken"] = 0.00125F,
                        ["VisionScore"] = 0.38F,
                        ["TotalMinionsKilled"] = 0.04F,
                        ["Winner"] = 20F
                    });

                var timeline = new ChallengeTimeline(new UtcNowDateTimeProvider(), new ChallengeDuration(TimeSpan.FromDays(1)));

                var challengeId1WithTwoEntries = ChallengeId.Parse("675fd61f-50a7-4268-8ed3-790428dd94c6");

                var challenge1WithTwoEntries = new Challenge(
                    challengeId1WithTwoEntries,
                    new ChallengeName("FREE CHALLENGE 1 (2)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Two,
                    timeline,
                    scoring);

                var challengeId2WithTwoEntries = ChallengeId.Parse("3e7326e7-f2b0-4da6-92fb-60aad19b7aff");

                var challenge2WithTwoEntries = new Challenge(
                    challengeId2WithTwoEntries,
                    new ChallengeName("FREE CHALLENGE 2 (2)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Two,
                    timeline,
                    scoring);

                var challengeId3WithTwoEntries = ChallengeId.Parse("c653e421-5439-4016-82d7-013a494a3eb0");

                var challenge3WithTwoEntries = new Challenge(
                    challengeId3WithTwoEntries,
                    new ChallengeName("FREE CHALLENGE 3 (2)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Two,
                    timeline,
                    scoring);

                var challengeId4WithTwoEntries = ChallengeId.Parse("0923e7c5-413b-47ec-a98b-4c97d2534acf");

                var challenge4WithTwoEntries = new Challenge(
                    challengeId4WithTwoEntries,
                    new ChallengeName("FREE CHALLENGE 4 (2)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Two,
                    timeline,
                    scoring);

                var challengeId5WithTwoEntries = ChallengeId.Parse("92c4c94f-a1f6-485d-b4bb-5555d5974419");

                var challenge5WithTwoEntries = new Challenge(
                    challengeId5WithTwoEntries,
                    new ChallengeName("FREE CHALLENGE 5 (2)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Two,
                    timeline,
                    scoring);

                var challengeId1WithFourEntries = ChallengeId.Parse("effc77f4-0961-4c3c-873b-b88abb1e97f2");

                var challenge1WithFourEntries = new Challenge(
                    challengeId1WithFourEntries,
                    new ChallengeName("FREE CHALLENGE 1 (4)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Four,
                    timeline,
                    scoring);

                var challengeId2WithFourEntries = ChallengeId.Parse("7e5290e0-f11b-4409-bcb5-211d115e33ee");

                var challenge2WithFourEntries = new Challenge(
                    challengeId2WithFourEntries,
                    new ChallengeName("FREE CHALLENGE 2 (4)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Four,
                    timeline,
                    scoring);

                var challengeId3WithFourEntries = ChallengeId.Parse("fb59ae64-771c-4cd7-b555-ec4bb14c69bf");

                var challenge3WithFourEntries = new Challenge(
                    challengeId3WithFourEntries,
                    new ChallengeName("FREE CHALLENGE 3 (4)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Four,
                    timeline,
                    scoring);

                var challengeId4WithFourEntries = ChallengeId.Parse("086d982a-fff6-493c-9294-eefba208ebd8");

                var challenge4WithFourEntries = new Challenge(
                    challengeId4WithFourEntries,
                    new ChallengeName("FREE CHALLENGE 4 (4)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Four,
                    timeline,
                    scoring);

                var challengeId5WithFourEntries = ChallengeId.Parse("2bd0dfc8-576f-4d5b-851c-5a914e186e2c");

                var challenge5WithFourEntries = new Challenge(
                    challengeId5WithFourEntries,
                    new ChallengeName("FREE CHALLENGE 5 (4)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Four,
                    timeline,
                    scoring);

                var challengeId1WithSixEntries = ChallengeId.Parse("4d15b0c6-d53d-4f75-a2ba-17c713c79677");

                var challenge1WithSixEntries = new Challenge(
                    challengeId1WithSixEntries,
                    new ChallengeName("FREE CHALLENGE 1 (6)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Six,
                    timeline,
                    scoring);

                var challengeId2WithSixEntries = ChallengeId.Parse("a8c93b67-6174-468f-8265-be9ca078bc96");

                var challenge2WithSixEntries = new Challenge(
                    challengeId2WithSixEntries,
                    new ChallengeName("FREE CHALLENGE 2 (6)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Six,
                    timeline,
                    scoring);

                _challengeRepository.Create(
                    new List<IChallenge>
                    {
                        challenge1WithTwoEntries,
                        challenge2WithTwoEntries,
                        challenge3WithTwoEntries,
                        challenge4WithTwoEntries,
                        challenge5WithTwoEntries,
                        challenge1WithFourEntries,
                        challenge2WithFourEntries,
                        challenge3WithFourEntries,
                        challenge4WithFourEntries,
                        challenge5WithFourEntries,
                        challenge1WithSixEntries,
                        challenge2WithSixEntries
                    });

                await _challengeRepository.CommitAsync(false);
            }
        }
    }
}
