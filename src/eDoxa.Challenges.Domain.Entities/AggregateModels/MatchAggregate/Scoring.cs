// Filename: ChallengeScoring.cs
// Date Created: 2019-04-14
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
//  
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using System.Collections.Generic;
using System.Linq;

namespace eDoxa.Challenges.Domain.Entities.AggregateModels.MatchAggregate
{
    public sealed class Scoring : Dictionary<StatName, StatWeighting>, IScoring
    {
        public Scoring()
        {
        }

        internal Scoring(IDictionary<string, float> scoring) : base(
            scoring.ToDictionary(
                pair => new StatName(pair.Key),
                pair => new StatWeighting(pair.Value)
            )
        )
        {
        }
    }
}