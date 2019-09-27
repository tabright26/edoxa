// Filename: IChallengeFakerFactory.cs
// Date Created: 2019-09-26
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers
{
    public interface IChallengeFakerFactory
    {
        IChallengeFaker CreateFaker(int? seed, ChallengeGame? game = null, ChallengeState? state = null);
    }
}
