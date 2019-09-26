// Filename: IChallengeFaker.cs
// Date Created: --
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers
{
    public interface IChallengeFaker
    {
        IChallenge FakeChallenge(string? ruleSets = null);

        IChallenge FakeChallenge(ChallengeModel model);
    }
}
