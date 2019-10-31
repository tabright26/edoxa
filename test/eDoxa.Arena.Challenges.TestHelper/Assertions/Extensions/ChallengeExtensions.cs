// Filename: ChallengeExtensions.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.TestHelper.Assertions.Extensions
{
    public static class ChallengeExtensions
    {
        public static ChallengeAssertions Should(this IChallenge challenge)
        {
            return new ChallengeAssertions(challenge);
        }
    }
}
