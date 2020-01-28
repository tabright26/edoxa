// Filename: ChallengeCollectionExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.AggregateModels;

namespace eDoxa.Challenges.TestHelper.Assertions.Extensions
{
    public static class ChallengeCollectionExtensions
    {
        public static ChallengeCollectionAssertions Should(this IEnumerable<IChallenge> challenges)
        {
            return new ChallengeCollectionAssertions(challenges);
        }
    }
}
