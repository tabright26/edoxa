// Filename: ChallengeExtensions.cs
// Date Created: 2019-06-29
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

using eDoxa.Arena.Challenges.Domain.AggregateModels;

namespace eDoxa.Arena.Challenges.Domain.Extensions
{
    public static class ChallengeExtensions
    {
        public static bool LastSynchronizationMoreThan(this IChallenge challenge, TimeSpan synchronizationInterval)
        {
            return challenge.SynchronizedAt + synchronizationInterval < DateTime.UtcNow;
        }
    }
}
