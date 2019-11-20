﻿// Filename: ChallengeCollectionExtensions.cs
// Date Created: 2019-10-02
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

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