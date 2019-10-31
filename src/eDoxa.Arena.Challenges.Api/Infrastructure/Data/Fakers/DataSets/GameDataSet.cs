// Filename: GameDataSet.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.DataSets
{
    public class GameDataSet
    {
        public GameDataSet(Faker faker)
        {
            Faker = faker;
        }

        internal Faker Faker { get; }

        public GameReference Reference(Game game)
        {
            if (game == Game.LeagueOfLegends)
            {
                return new GameReference(Faker.Random.Long(1000000000, 9999999999));
            }

            throw new ArgumentException(nameof(game));
        }

        public IGameStats Stats(Game game)
        {
            if (game == Game.LeagueOfLegends)
            {
                return new GameStats(
                    new
                    {
                        Kills = Faker.Random.Int(0, 40),
                        Deaths = Faker.Random.Int(0, 15),
                        Assists = Faker.Random.Int(0, 50),
                        TotalDamageDealtToChampions = Faker.Random.Int(10000, 500000),
                        TotalHeal = Faker.Random.Int(10000, 350000)
                    });
            }

            throw new ArgumentException(nameof(game));
        }
    }
}
