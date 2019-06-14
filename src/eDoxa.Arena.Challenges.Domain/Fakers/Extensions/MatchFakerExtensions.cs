// Filename: MatchFakerExtensions.cs
// Date Created: 2019-06-13
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

using Bogus;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.Abstractions.Adapters;
using eDoxa.Arena.Challenges.Domain.Abstractions.Strategies;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate;
using eDoxa.Arena.Services.LeagueOfLegends.Dtos;
using eDoxa.Seedwork.Common.Enumerations;
using eDoxa.Seedwork.Common.Extensions;

using Moq;

namespace eDoxa.Arena.Challenges.Domain.Fakers.Extensions
{
    public static class MatchFakerExtensions
    {
        public static MatchId MatchId(this Faker faker)
        {
            return AggregateModels.MatchAggregate.MatchId.FromGuid(faker.Random.Guid());
        }

        public static MatchReference MatchReference(this Faker faker, Game game)
        {
            if (game == Game.LeagueOfLegends)
            {
                return new MatchReference(faker.Random.Long(1000000000, 9999999999));
            }

            throw new ArgumentNullException(nameof(game));
        }

        public static DateTime MatchTimestamp(this Faker faker)
        {
            return faker.Date.Recent(1, DateTime.UtcNow.DateKeepHours());
        }

        public static IMatchStats MatchStats(this Faker faker, Game game)
        {
            if (game == Game.LeagueOfLegends)
            {
                return new MatchStats(
                    new
                    {
                        Kills = faker.Random.Int(0, 40),
                        Deaths = faker.Random.Int(0, 15),
                        Assists = faker.Random.Int(0, 50),
                        TotalDamageDealtToChampions = faker.Random.Int(10000, 500000),
                        TotalHeal = faker.Random.Int(10000, 350000)
                    }
                );
            }

            throw new ArgumentNullException(nameof(game));
        }

        public static IMatchStatsAdapter MatchStatsAdapter(this Faker faker, Game game)
        {
            var mock = new Mock<IMatchStatsAdapter>();

            var matchStats = faker.MatchStats(game);

            mock.SetupGet(adapter => adapter.MatchStats).Returns(matchStats);

            return mock.Object;
        }

        public static IScoring Scoring(this Faker faker, Game game)
        {
            if (game == Game.LeagueOfLegends)
            {
                return new Scoring(
                    new HashSet<ScoringItem>
                    {
                        new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Kills)), new StatWeighting(4F)),
                        new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Deaths)), new StatWeighting(-3F)),
                        new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.Assists)), new StatWeighting(3F)),
                        new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalDamageDealtToChampions)), new StatWeighting(0.00015F)),
                        new ScoringItem(new StatName(nameof(LeagueOfLegendsParticipantStatsDto.TotalHeal)), new StatWeighting(0.0008F))
                    }
                );
            }

            throw new ArgumentNullException(nameof(game));
        }

        public static IScoringStrategy ScoringStrategy(this Faker faker, Game game)
        {
            var mock = new Mock<IScoringStrategy>();

            var scoring = faker.Scoring(game);

            mock.SetupGet(strategy => strategy.Scoring).Returns(scoring);

            return mock.Object;
        }
    }
}
