// Filename: ChallengeExtensions.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2020, eDoxa. All rights reserved.

using eDoxa.Challenges.Domain.AggregateModels;

namespace eDoxa.Challenges.TestHelper.Assertions.Extensions
{
    public static class ChallengeExtensions
    {
        public static ChallengeAssertions Should(this IChallenge challenge)
        {
            return new ChallengeAssertions(challenge);
        }
    }
}
