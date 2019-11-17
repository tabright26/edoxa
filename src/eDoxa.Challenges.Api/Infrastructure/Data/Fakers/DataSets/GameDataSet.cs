// Filename: GameDataSet.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using Bogus;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Fakers.DataSets
{
    public class GameDataSet
    {
        public GameDataSet(Faker faker)
        {
            Faker = faker;
        }

        internal Faker Faker { get; }

        public GameUuid Uuid()
        {
            return new GameUuid(Faker.Random.Long(1000000000, 9999999999));
        }

        public IDictionary<string, double> Stats()
        {
            return new Dictionary<string, double>
            {
                ["StatName1"] = Faker.Random.Int(0, 40),
                ["StatName2"] = Faker.Random.Int(0, 15),
                ["StatName3"] = Faker.Random.Int(0, 50),
                ["StatName4"] = Faker.Random.Int(10000, 500000),
                ["StatName5"] = Faker.Random.Int(10000, 350000)
            };
        }
    }
}
