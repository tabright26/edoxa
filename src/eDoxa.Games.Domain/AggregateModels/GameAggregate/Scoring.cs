// Filename: ScoringDto.cs
// Date Created: 2019-11-25
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.

using System.Collections.Generic;

namespace eDoxa.Games.Domain.AggregateModels.GameAggregate
{
    public sealed class Scoring : Dictionary<string, float>
    {
        public Scoring(IDictionary<string, float> scoring) : base(scoring)
        {
        }
    }
}
