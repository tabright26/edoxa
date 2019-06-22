// Filename: MatchFaker.cs
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
using eDoxa.Arena.Challenges.Domain.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.Fakers.Providers;

namespace eDoxa.Arena.Challenges.Domain.Fakers
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

                    match.SnapshotStats(scoring, faker.Match().Stats(game));

                    return match;
                }
            );
        }
    }
}
