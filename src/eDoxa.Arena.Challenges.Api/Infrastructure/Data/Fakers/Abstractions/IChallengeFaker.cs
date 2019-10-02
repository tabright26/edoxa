// Filename: IChallengeFaker.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Arena.Challenges.Domain.AggregateModels;
using eDoxa.Arena.Challenges.Infrastructure.Models;

namespace eDoxa.Arena.Challenges.Api.Infrastructure.Data.Fakers.Abstractions
{
    public interface IChallengeFaker
    {
        IReadOnlyCollection<IChallenge> FakeChallenges(int count, string? ruleSets = null);

        IChallenge FakeChallenge(string? ruleSets = null);

        IChallenge FakeChallenge(ChallengeModel model);
    }
}
