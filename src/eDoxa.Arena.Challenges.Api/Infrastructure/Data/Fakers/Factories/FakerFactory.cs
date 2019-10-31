// Filename: FakerFactory.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Abstractions;
using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;
using eDoxa.Seedwork.Domain.Miscs;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Factories
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
