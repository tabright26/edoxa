// Filename: MatchScore.cs
// Date Created: 2019-04-21
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Linq;

using eDoxa.Arena.Domain.Abstractions;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public sealed class MatchScore : Score
    {
        internal MatchScore(Match match) : base(match.Stats.Sum(stat => stat.Score))
        {
        }
    }
}