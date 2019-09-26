// Filename: ChallengeFakerFactory.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers
{
    public sealed class ChallengeFakerFactory : IChallengeFakerFactory
    {
        public IChallengeFaker CreateInstance(ChallengeGame? game, ChallengeState? state, int? seed = null)
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
