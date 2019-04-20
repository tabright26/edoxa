// Filename: StatScoreAdapter.cs
// Date Created: 2019-04-19
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System;

namespace eDoxa.Challenges.Domain.AggregateModels.ChallengeAggregate.Adapters
{
    internal sealed class StatScoreAdapter : IScoreAdapter
    {
        private readonly Stat _stat;

        public StatScoreAdapter(Stat stat)
        {
            _stat = stat;
        }

        public Score Score => new Score(Convert.ToDecimal(_stat.Value) * Convert.ToDecimal(_stat.Weighting));
    }
}