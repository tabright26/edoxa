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
using eDoxa.Arena.Challenges.Api.Application.Fakers.Providers;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Application.Fakers
{
    internal sealed class MatchFaker : Faker<Match>
    {
        public MatchFaker(ChallengeGame game, IScoring scoring, DateTime synchronizedAt)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var match = new Match(faker.Match().GameId(game), new FakeDateTimeProvider(synchronizedAt));

                    match.SetEntityId(faker.Match().Id());

                    match.Snapshot(faker.Match().Stats(game), scoring);

                    return match;
                }
            );
        }
    }
}
