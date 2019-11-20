// Filename: MatchDataSet.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Fakers.DataSets
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
    }
}
