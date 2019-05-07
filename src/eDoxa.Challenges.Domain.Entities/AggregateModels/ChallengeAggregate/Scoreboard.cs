// Filename: ChallengeScoreboard.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;

using eDoxa.Challenges.Domain.Entities.Abstractions;
using eDoxa.Functional.Maybe;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.ChallengeAggregate
{
    public sealed class Scoreboard : Dictionary<UserId, Option<Score>>, IScoreboard
    {
    }
}