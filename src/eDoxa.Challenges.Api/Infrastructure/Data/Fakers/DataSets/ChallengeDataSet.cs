// Filename: ChallengeDataSet.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using Bogus;

using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Fakers.DataSets
{
    public class ChallengeDataSet
    {
        public ChallengeDataSet(Faker faker)
        {
            Faker = faker;
        }

        public Faker Faker { get; }

        public ChallengeId Id()
        {
            return ChallengeId.FromGuid(Faker.Random.Guid());
        }

        public ChallengeName Name()
        {
            return new ChallengeName(Faker.PickRandom("Daily Challenge", "Monthly Challenge", "Weekly Challenge"));
        }

        public Game Game(Game? game = null)
        {
            return game ?? Faker.PickRandom(Seedwork.Domain.Miscs.Game.GetEnumerations());
        }

        public BestOf BestOf()
        {
            return Faker.PickRandom(ValueObject.GetValues<BestOf>());
        }

        public Entries Entries()
        {
            return Faker.PickRandom(ValueObject.GetValues<Entries>());
        }

        public ChallengeDuration Duration()
        {
            return Faker.PickRandom(ValueObject.GetValues<ChallengeDuration>());
        }

        public ChallengeState State(ChallengeState? state = null)
        {
            return state ?? Faker.PickRandom(ChallengeState.GetEnumerations());
        }
    }
}
