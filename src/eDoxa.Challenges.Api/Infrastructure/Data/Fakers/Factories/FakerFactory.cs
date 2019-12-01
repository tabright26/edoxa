// Filename: FakerFactory.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Abstractions;
using eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Misc;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Factories
{
    public sealed class FakerFactory
    {
        public IChallengeFaker CreateChallengeFaker(int? seed, Game? game = null, ChallengeState? state = null)
        {
            var faker = new ChallengeFaker(game, state);

            if (seed.HasValue)
            {
                faker.UseSeed(seed.Value);
            }

            return faker;
        }
    }
}
