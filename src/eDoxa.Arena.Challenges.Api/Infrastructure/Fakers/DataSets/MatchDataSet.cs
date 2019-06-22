// Filename: MatchDataSet.cs
// Date Created: 2019-06-22
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Domain.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Fakers.DataSets
{
    public class MatchDataSet
    {
        public MatchDataSet(Faker faker)
        {
            Faker = faker;
        }

        internal Faker Faker { get; }

        public MatchId Id()
        {
            return MatchId.FromGuid(Faker.Random.Guid());
        }

        public DateTime SynchronizedAt(DateTime? startedAt)
        {
            return Faker.Date.Soon(1, startedAt);
        }

        public GameMatchId GameId(ChallengeGame game)
        {
            if (game == ChallengeGame.LeagueOfLegends)
            {
                return new GameMatchId(Faker.Random.Long(1000000000, 9999999999));
            }

            throw new ArgumentNullException(nameof(game));
        }

        public IMatchStats Stats(ChallengeGame game)
        {
            if (game == ChallengeGame.LeagueOfLegends)
            {
                return new MatchStats(
                    new
                    {
                        Kills = Faker.Random.Int(0, 40),
                        Deaths = Faker.Random.Int(0, 15),
                        Assists = Faker.Random.Int(0, 50),
                        TotalDamageDealtToChampions = Faker.Random.Int(10000, 500000),
                        TotalHeal = Faker.Random.Int(10000, 350000)
                    }
                );
            }

            throw new ArgumentNullException(nameof(game));
        }
    }
}
