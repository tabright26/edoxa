// Filename: ChallengeTimelineDataSet.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Api.Application.Fakers.DataSets
{
    public class ChallengeTimelineDataSet
    {
        public ChallengeTimelineDataSet(Faker faker)
        {
            Faker = faker;
        }

        internal Faker Faker { get; }

        public ChallengeDuration Duration()
        {
            return Faker.PickRandom(ValueObject.GetValues<ChallengeDuration>());
        }
    }
}
