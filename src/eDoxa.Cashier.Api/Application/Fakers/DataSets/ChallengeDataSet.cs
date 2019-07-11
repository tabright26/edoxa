// Filename: ChallengeDataSet.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Cashier.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Cashier.Api.Application.Fakers.DataSets
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
    }
}
