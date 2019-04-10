// Filename: IChallengePrizeBreakdown.cs
// Date Created: 2019-03-20
// 
// ============================================================
// Copyright © 2019, Francis Quenneville
// All rights reserved.
// 
// This file is subject to the terms and conditions defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

namespace eDoxa.Challenges.Domain
{
    public interface IChallengePrizeBreakdown : IReadOnlyDictionary<string, decimal>
    {
        IReadOnlyDictionary<Guid, decimal?> SnapshotUserPrizes(IChallengeScoreboard scoreboard);
    }
}