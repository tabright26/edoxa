// Filename: StatWeighting.cs
// Date Created: 2019-05-20
// 
// ================================================
// Copyright © 2019, eDoxa. All rights reserved.
// 
// This file is subject to the terms and conditions
// defined in file 'LICENSE.md', which is part of
// this source code package.

using eDoxa.Seedwork.Domain.Aggregate;

namespace eDoxa.Arena.Challenges.Domain.AggregateModels.MatchAggregate
{
    public class StatWeighting : TypedObject<StatWeighting, float>
    {
        public StatWeighting(float weighting)
        {
            Value = weighting;
        }

        public override string ToString()
        {
            return Value.ToString("R");
        }
    }
}
