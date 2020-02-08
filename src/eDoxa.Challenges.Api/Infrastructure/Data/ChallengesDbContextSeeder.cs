// Filename: ChallengesDbContextSeeder.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using eDoxa.Challenges.Api.Infrastructure.Data.Storage;
using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Challenges.Infrastructure;
using eDoxa.Challenges.Infrastructure.Extensions;
using eDoxa.Challenges.Infrastructure.Models;
using eDoxa.Seedwork.Application.SqlServer.Abstractions;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Misc;

using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace eDoxa.Challenges.Api.Infrastructure.Data
{
    internal sealed class ChallengesDbContextSeeder : DbContextSeeder
    {
        public ChallengesDbContextSeeder(ChallengesDbContext context, IWebHostEnvironment environment, ILogger<ChallengesDbContextSeeder> logger) : base(
            context,
            environment,
            logger)
        {
            Challenges = context.Set<ChallengeModel>();
        }

        private DbSet<ChallengeModel> Challenges { get; }

        protected override async Task SeedDevelopmentAsync()
        {
            await this.SeedTestChallengesAsync();
        }
        
        protected override async Task SeedStagingAsync()
        {
            await this.SeedChallengesAsync();
        }

        private async Task SeedTestChallengesAsync()
        {
            if (!await Challenges.AnyAsync())
            {
                Challenges.AddRange(FileStorage.Challenges.Select(challenge => challenge.ToModel()));

                await this.CommitAsync();
            }
        }

        private async Task SeedChallengesAsync()
        {
            var scoring = new Scoring(
                new Dictionary<string, float>
                {
                    ["Kills"] = 4.5F,
                    ["Deaths"] = -4F,
                    ["Assists"] = 3.5F,
                    ["TotalDamageDealtToChampions"] = 0.0009F,
                    ["TotalDamageTaken"] = 0.00125F,
                    ["TotalMinionsKilled"] = 0.04F,
                    ["VisionScore"] = 0.38F,
                    ["Winner"] = 20F
                });

            var timeline = new ChallengeTimeline(new UtcNowDateTimeProvider(), new ChallengeDuration(TimeSpan.FromDays(1)));

            var challenges = new List<IChallenge>
            {
                new Challenge(
                    ChallengeId.Parse("d53b366f-e717-43d4-ac12-6e13d37f5cef"),
                    new ChallengeName("2$ CHALLENGE BEST OF 1 (2)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Two,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("369ae69d-b10d-4d72-84ba-698691646ba6"),
                    new ChallengeName("3$ CHALLENGE BEST OF 1 (2)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Two,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("eb76fa60-700f-4dce-b312-d69897563437"),
                    new ChallengeName("2$ CHALLENGE BEST OF 1 (4)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Four,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("82592581-e6ac-41e0-9c61-773d924f233d"),
                    new ChallengeName("3$ CHALLENGE BEST OF 1 (4)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Four,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("9457ae9a-4e5c-436f-b10f-33134af68439"),
                    new ChallengeName("2$ CHALLENGE BEST OF 1 (6)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Six,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("91f6d007-b458-4f1c-9814-755b32059e00"),
                    new ChallengeName("3$ CHALLENGE BEST OF 1 (6)"),
                    Game.LeagueOfLegends,
                    BestOf.One,
                    Entries.Six,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("4ecb13a4-0742-4140-93b0-27ee582e5cab"),
                    new ChallengeName("2$ CHALLENGE BEST OF 3 (2)"),
                    Game.LeagueOfLegends,
                    BestOf.Three,
                    Entries.Two,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("fa38f697-2ef3-40e9-a165-d62c3cc750a8"),
                    new ChallengeName("3$ CHALLENGE BEST OF 3 (2)"),
                    Game.LeagueOfLegends,
                    BestOf.Three,
                    Entries.Two,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("ac6851b4-2cb7-42ab-bf44-fb197d21221b"),
                    new ChallengeName("2$ CHALLENGE BEST OF 3 (4)"),
                    Game.LeagueOfLegends,
                    BestOf.Three,
                    Entries.Four,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("bb5f6e0c-ada7-47b4-9d24-a3c9ec7df034"),
                    new ChallengeName("3$ CHALLENGE BEST OF 3 (4)"),
                    Game.LeagueOfLegends,
                    BestOf.Three,
                    Entries.Four,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("6ec217f7-3d6a-41c2-b2eb-4cc8799d2af5"),
                    new ChallengeName("2$ CHALLENGE BEST OF 3 (6)"),
                    Game.LeagueOfLegends,
                    BestOf.Three,
                    Entries.Six,
                    timeline,
                    scoring),
                new Challenge(
                    ChallengeId.Parse("7d96b314-8d5b-4393-9257-9c0e2cf7c0f1"),
                    new ChallengeName("3$ CHALLENGE BEST OF 3 (6)"),
                    Game.LeagueOfLegends,
                    BestOf.Three,
                    Entries.Six,
                    timeline,
                    scoring)
            };

            Challenges.AddRange(challenges.Where(challenge => Challenges.All(x => x.Id != challenge.Id)).Select(challenge => challenge.ToModel()));

            await this.CommitAsync();
        }
    }
}
