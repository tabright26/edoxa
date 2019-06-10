// Filename: ChallengeFaker.cs
// Date Created: 2019-06-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using Bogus;

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Common.Extensions;

namespace eDoxa.Arena.Challenges.Api.Application.Data.Fakers
{
    public class ChallengeFaker : Faker<Challenge>
    {
        public ChallengeFaker()
        {
            this.UseSeed();
        }

        public Challenge FakeChallenge()
        {
            return this.Generate();
        }
    }
}
