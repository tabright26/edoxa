// Filename: MatchFaker.cs
// Date Created: 2019-06-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Api.Application.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Api.Application.Fakers
{
    internal sealed class MatchFaker : Faker<IMatch>
    {
        public MatchFaker(ChallengeGame game, IScoring scoring, DateTime synchronizedAt)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var match = new StatMatch(scoring, faker.Match().Stats(game), faker.Match().GameReference(game), new DateTimeProvider(synchronizedAt));

                    match.SetEntityId(faker.Match().Id());

                    return match;
                }
            );
        }
    }
}
