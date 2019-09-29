// Filename: ChallengeExtensions.cs
// Date Created: 2019-09-28
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.TestHelpers.Assertions.Extensions
{
    public static class ChallengeExtensions
    {
        public static ChallengeAssertions Should(this IChallenge challenge)
        {
            return new ChallengeAssertions(challenge);
        }
    }
}
