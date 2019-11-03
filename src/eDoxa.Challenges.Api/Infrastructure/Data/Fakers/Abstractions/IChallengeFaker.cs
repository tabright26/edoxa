// Filename: IChallengeFaker.cs
// Date Created: 2019-10-06
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels;
using eDoxa.Challenges.Infrastructure.Models;

namespace eDoxa.Challenges.Api.Infrastructure.Data.Fakers.Abstractions
{
    public interface IChallengeFaker
    {
        IReadOnlyCollection<IChallenge> FakeChallenges(int count, string? ruleSets = null);

        IChallenge FakeChallenge(string? ruleSets = null);

        IChallenge FakeChallenge(ChallengeModel model);
    }
}
