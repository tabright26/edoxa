// Filename: MatchFaker.cs
// Date Created: 2019-07-12
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System;

using Bogus;

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Extensions;
using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers
{
    internal sealed class MatchFaker : Faker<IMatch>
    {
        public MatchFaker(ChallengeGame game, IScoring scoring, DateTime synchronizedAt)
        {
            this.CustomInstantiator(
                faker =>
                {
                    var match = new StatMatch(scoring, faker.Game().Stats(game), faker.Game().Reference(game), new DateTimeProvider(synchronizedAt));

                    match.SetEntityId(faker.Match().Id());

                    return match;
                }
            );
        }
    }
}
