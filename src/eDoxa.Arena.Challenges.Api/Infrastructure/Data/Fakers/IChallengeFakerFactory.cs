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
        IChallengeFaker CreateInstance(ChallengeGame? game, ChallengeState? state, int? seed = null);
    }
}
