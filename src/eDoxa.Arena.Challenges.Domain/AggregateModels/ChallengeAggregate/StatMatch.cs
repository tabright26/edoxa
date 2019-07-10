// Filename: StatMatch.cs
// Date Created: 2019-07-09
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.ChallengeAggregate
{
    public sealed class StatMatch : Match
    {
        public StatMatch(IScoring scoring, IGameStats stats, GameReference gameReference, IDateTimeProvider synchronizedAt) : base(scoring.Map(stats), gameReference, synchronizedAt)
        {
        }
    }
}
