// Filename: IChallengePayout.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;
using System.Collections.Generic;

namespace eDoxa.Challenges.Domain
{
    public interface IChallengePayout : IReadOnlyDictionary<string, decimal>
    {
        IReadOnlyDictionary<Guid, decimal?> Snapshot(IChallengeScoreboard scoreboard);
    }
}